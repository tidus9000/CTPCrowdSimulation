using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Vector2 m_velocity;
    [SerializeField] float m_maxspeed;
    [SerializeField] float slowdown;
    [SerializeField] float nearbyRadius = 1;
    [SerializeField] float separation = 1;
    [SerializeField]Vector2 averagePosition = Vector2.zero;

    bool m_forceAdded = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;

        if (!m_forceAdded)
        {
            bool nearby = false;

            //getting average direction of surrounding agents
            //and average position
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, nearbyRadius);
            int i = 0;
            Vector2 averageDirection = Vector2.zero;
            Vector2 separationForce = Vector2.zero;
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Agent") && col.gameObject != this.gameObject)
                {
                    GameObject agent = col.gameObject;
                    averageDirection += agent.GetComponent<Agent>().m_velocity;
                    averagePosition.x += agent.transform.position.x;
                    averagePosition.y += agent.transform.position.y;

                    separationForce.x += transform.position.x - agent.transform.position.x;
                    separationForce.y += transform.position.y - agent.transform.position.y;


                    i++;
                    nearby = true;
                }
            }

            if (nearby)
            {
                //apply alignment
                averageDirection /= i;
                averagePosition /= i;
                m_velocity += averageDirection;

                //apply cohesion
                Vector2 force = Vector2.zero;
                force.x = averagePosition.x - transform.position.x;
                force.y = averagePosition.y - transform.position.y;
                m_velocity += force;

                //apply separation
                m_velocity += separationForce * separation;
            }


            m_velocity *= slowdown;

            if (m_velocity.magnitude <= 0.1)
            {
                m_velocity = Vector2.zero;
            }
            averagePosition = Vector2.zero;
        }


        m_velocity = Vector3.ClampMagnitude(m_velocity, m_maxspeed);
        position += m_velocity * Time.deltaTime;
        transform.position = position;

        m_forceAdded = false;
    }

#if (UNITY_EDITOR)

    void OnDrawGizmos()
    {
        Vector3 normal = m_velocity;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + normal);
        UnityEditor.Handles.DrawWireDisc(averagePosition, Vector3.forward, 0.1f);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, nearbyRadius);
    }

#endif


    public void AddForce(Vector2 _force)
    {
        m_forceAdded = true;
        m_velocity += _force;
    }

    public void Slowdown()
    {
       if (m_velocity.magnitude > 0.5)
        {
            m_velocity *= 0.75f;
        }
       else
        {
            m_velocity = Vector2.zero;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Vector2 m_velocity;
    [SerializeField] float m_maxspeed;
    [SerializeField] float slowdown;
    [SerializeField] float nearbyRadius = 1;

    bool m_forceAdded = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;

        if (!m_forceAdded)
        {
            //getting average direction of surrounding agents
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, nearbyRadius);
            int i = 0;
            Vector2 averageDirection = Vector2.zero;
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Agent"))
                {
                    GameObject agent = col.gameObject;
                    averageDirection += agent.GetComponent<Agent>().m_velocity;
                    i++;
                }
            }
            averageDirection /= i;
            m_velocity = averageDirection;

            m_velocity *= slowdown;

            if (m_velocity.magnitude <= 0.1)
            {
                m_velocity = Vector2.zero;
            }
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

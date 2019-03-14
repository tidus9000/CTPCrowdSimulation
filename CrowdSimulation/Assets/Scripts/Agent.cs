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

    //variables for behaviour when agent is panicked
    [SerializeField] bool m_panicked = false;
    [SerializeField] float m_maxPanickedSpeed;
    [SerializeField] float m_maxPanicTime;
    float m_defaultMaxSpeed;
    float panickedTimer = 0f;

    [SerializeField] bool monitor = false;

    bool m_forceAdded = false;

	// Use this for initialization
	void Start () {
        m_defaultMaxSpeed = m_maxspeed;
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
                    bool obstacle = false;
                   
                    GameObject agent = col.gameObject;

                    if (!obstacle)
                    {
                        averageDirection += agent.GetComponent<Agent>().m_velocity;
                        averagePosition.x += agent.transform.position.x;
                        averagePosition.y += agent.transform.position.y;

                        separationForce.x += transform.position.x - agent.transform.position.x;
                        separationForce.y += transform.position.y - agent.transform.position.y;


                        i++;
                        nearby = true;
                    }


                    //if the agent is panicked, spread the panic to nearby agents too
                    if (m_panicked)
                    {
                        agent.GetComponent<Agent>().panic();
                    }
                }
            }

            if (nearby)
            {
                averageDirection += GetComponent<Agent>().m_velocity;
                averagePosition += new Vector2(transform.position.x, transform.position.y);
                //apply alignment
                averageDirection /= i + 1;
                averagePosition /= i + 1;
                m_velocity += averageDirection;

                //apply cohesion
                Vector2 force = Vector2.zero;
                force.x = averagePosition.x - transform.position.x;
                force.y = averagePosition.y - transform.position.y;
                m_velocity += force;

                //apply separation
                m_velocity += separationForce * separation;

                if (monitor)
                {
                    Debug.Log(gameObject.name + " number nearby: " + i);
                    Debug.Log(gameObject.name + " average position: " + i);
                    Debug.Log(gameObject.name + " average direction: " + i);
                }
            }

            //apply any slowdown
            m_velocity *= slowdown;

            //stop agent if it is going slow enough
            if (m_velocity.magnitude <= 0.1)
            {
                m_velocity = Vector2.zero;
            }
            //reset averageposition for next frame
            averagePosition = Vector2.zero;
        }

        if (m_panicked)
        {
            panickedTimer += Time.deltaTime;
            if (panickedTimer >= m_maxPanicTime)
            {
                panickedTimer = 0;
                m_panicked = false;
                m_maxspeed = m_defaultMaxSpeed;
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
        UnityEditor.Handles.DrawWireDisc(averagePosition, Vector3.forward, 0.1f);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, nearbyRadius);
    }

#endif

    public void panic()
    {
        panickedTimer = 0f;
        m_maxspeed = m_maxPanickedSpeed;
        m_panicked = true;
    }

    public void AddForce(Vector2 _force)
    {
        Vector2 sp = transform.position;
        Vector2 ep = new Vector2(transform.position.x, transform.position.y) + _force;
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

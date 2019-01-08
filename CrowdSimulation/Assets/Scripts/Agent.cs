using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Vector2 m_velocity;
    [SerializeField] float m_maxspeed;
    [SerializeField] float slowdown;
    [SerializeField] float nearbyRadius = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;
        m_velocity = Vector3.ClampMagnitude(m_velocity, m_maxspeed);
        position += m_velocity * Time.deltaTime;
        transform.position = position;

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


    public void AddForce(Vector2 _force)
    {
        m_velocity += _force;
    }

    public void Slowdown()
    {
       
    }
}

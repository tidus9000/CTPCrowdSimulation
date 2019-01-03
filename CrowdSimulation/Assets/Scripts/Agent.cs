using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    [SerializeField] Vector2 m_velocity;
    [SerializeField] float m_maxspeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;
        m_velocity = Vector3.ClampMagnitude(m_velocity, m_maxspeed);
        position += m_velocity * Time.deltaTime;
        transform.position = position;
	}

    public void AddForce(Vector2 _force)
    {
        m_velocity += _force;
    }

    public void Slowdown()
    {
        m_velocity *= 0.75f;

        if (m_velocity.magnitude <= 0.1)
        {
            m_velocity = Vector2.zero;
        }
    }
}

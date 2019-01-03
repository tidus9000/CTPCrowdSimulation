using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    [SerializeField] float m_radius;

    GameObject[] agents;

	// Use this for initialization
	void Start () {
        agents = GameObject.FindGameObjectsWithTag("Agent");
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject agent in agents)
        {
            Vector2 heading = transform.position - agent.transform.position;

            float distanceSqr = (transform.position - agent.transform.position).sqrMagnitude;
            if (distanceSqr < (m_radius * m_radius))
            {
                agent.GetComponent<Agent>().Slowdown();
            }
            else
            {
                agent.GetComponent<Agent>().AddForce(heading);
            }
        }
	}

#if (UNITY_EDITOR)

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, m_radius);
    }

#endif
}

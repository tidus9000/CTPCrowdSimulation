using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeller : MonoBehaviour {

    [SerializeField] float m_radius;
    [SerializeField] bool m_hazard;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject agent = collision.gameObject;
        if (agent.CompareTag("Agent"))
        {
            Vector2 heading = transform.position - agent.transform.position;

            float distanceSqr = (transform.position - agent.transform.position).sqrMagnitude;

            if (m_hazard)
            {
                collision.gameObject.GetComponent<Agent>().panic();
            }

            if (distanceSqr < (m_radius * m_radius))
            {
                //agent.GetComponent<Agent>().Slowdown();
            }
            else
            {
                agent.GetComponent<Agent>().AddForce(-heading.normalized * 0.25f);
            }
        }
    }
}

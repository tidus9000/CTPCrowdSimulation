using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {

    [SerializeField]Vector2 previousPosition;
    [SerializeField]Vector2 direction;

    [SerializeField] float inFrontMulfactor = 1;
    [SerializeField] float behindMulfactor = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;
        direction = position - previousPosition;
        previousPosition = transform.position;

        Gizmos.color = Color.white;
        Vector3 direction3D = direction;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //check its an agent
        GameObject col = collision.gameObject;

        if (col.CompareTag("Agent"))
        {
            //apply force proportional to the direction
            //there are two things to take into consideration when applying the force:
            //1. how close is the agent? we want a closer agent to be more affected by the leader
            //2. where is the agent? if they are in front, they are more likely to be slower to get behind the leader


            //dot product of direction and positions gives us distance of agent from direction.
            //if this is <0 that means it's behind us

            if (direction.magnitude >= 0.01)
            {
                float dot = Vector2.Dot(direction.normalized, (col.transform.position - transform.position).normalized);

                Vector2 force = direction;

                if (dot > 0)
                {
                    force *= inFrontMulfactor;
                }
                else if (dot <= 0)
                {
                    force *= behindMulfactor;
                }

                col.GetComponent<Agent>().AddForce(force);
            }
            else
            {
                col.GetComponent<Agent>().Slowdown();
            }
        }
    }

#if (UNITY_EDITOR)

    void OnDrawGizmos()
    {
        Vector3 normal = Vector2.Perpendicular(direction.normalized);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position - normal, transform.position + normal);
    }

#endif
}

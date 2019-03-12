using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    [SerializeField] float affectdistance = 1;
    [SerializeField] float force = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(transform.localScale.x + affectdistance, transform.localScale.y + affectdistance), 0.0f);
        foreach (Collider2D col in colliders)
        {
            //if (col.tag == "Agent")
            //{
            //    var wallCollider = GetComponent<Collider2D>();
                
                
            //    //this bit doesnt work for some reason.
            //    Vector2 heading = wallCollider.bounds.ClosestPoint(transform.position);
                
            //    Debug.Log(heading);
            //    heading.x -= col.gameObject.transform.position.x;
            //    heading.y -= col.gameObject.transform.position.y;
            //    // We then get the opposite (-Vector3) and normalize it
            //    heading = heading.normalized;
            //    // And finally we add force in the direction of dir and multiply it by force. 
            //    // This will push back the agent
            //    col.gameObject.GetComponent<Agent>().AddForce(heading * force);
            //}
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // force is how forcefully we will push the agent
        float force = 5;

        // If the object we hit is the agent
        if (collision.gameObject.tag == "Agent")
        {
            // Calculate Angle Between the collision point and the player
            Vector2 dir = collision.contacts[0].point;
            dir.x -= collision.transform.position.x;
            dir.y -= collision.transform.position.y;
            Vector2 heading = collision.contacts[0].point;
            heading.x -= collision.gameObject.transform.position.x;
            heading.y -= collision.gameObject.transform.position.y;
            // We then get the opposite (-Vector3) and normalize it
            heading = -heading.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the agent
            collision.gameObject.GetComponent<Agent>().AddForce(heading * force);
        }
    }

#if (UNITY_EDITOR)

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x + affectdistance, transform.localScale.y + affectdistance, transform.localScale.z));
    }

#endif
}

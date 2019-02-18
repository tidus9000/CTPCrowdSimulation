using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // force is how forcefully we will push the agent
        float force = 2;

        // If the object we hit is the enemy
        if (collision.gameObject.tag == "Agent")
        {
            // Calculate Angle Between the collision point and the player
            Vector2 dir = collision.contacts[0].point;
            dir.x -= collision.transform.position.x;
            dir.y -= collision.transform.position.y;

            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the agent
            collision.gameObject.GetComponent<Agent>().AddForce(dir * force);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour {

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
            Destroy(agent);
        }
    }
}

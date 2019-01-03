using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    GameObject[] agents;

	// Use this for initialization
	void Start () {
        agents = GameObject.FindGameObjectsWithTag("Agent");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject agent in agents)
            {
                Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pz.z = 0;
                Vector2 heading = pz - agent.transform.position;
                heading.Normalize();
                agent.GetComponent<Agent>().AddForce(heading);
            }
        }
	}
}

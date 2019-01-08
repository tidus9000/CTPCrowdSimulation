using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = transform.position;

        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");

        move.x += movex * 0.1f;
        move.y -= movey * 0.1f;
        transform.position = move;
	}
}

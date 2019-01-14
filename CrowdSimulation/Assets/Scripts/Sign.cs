using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {

    [SerializeField]float m_InfluenceRadius = 1;
    [SerializeField]float m_force;
    [SerializeField]float m_angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        //check its an agent
        GameObject col = collision.gameObject;

        if (col.CompareTag("Agent"))
        {
            Quaternion rotation = Quaternion.Euler(0, 0, m_angle);
            Vector3 myVector = Vector3.up;
            Vector3 rotateVector = rotation * myVector;
            rotateVector.Normalize();
            rotateVector *= m_force;
            col.GetComponent<Agent>().AddForce(rotateVector);
        }
    }

#if (UNITY_EDITOR)

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, m_InfluenceRadius);
        Quaternion rotation = Quaternion.Euler(0, 0, m_angle);
        Vector3 myVector = Vector3.up;
        Vector3 rotateVector = rotation * myVector;
        Gizmos.DrawLine(transform.position, transform.position + rotateVector);
    }

#endif
}

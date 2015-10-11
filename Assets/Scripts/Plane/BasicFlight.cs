using UnityEngine;
using System.Collections;

public class BasicFlight : MonoBehaviour
{

	private Rigidbody m_Rigidbody;

	// Use this for initialization
	void Start ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		transform.position = transform.position + transform.forward * 6 * Time.deltaTime;
		transform.RotateAround (Vector3.zero, Vector3.up, -60 * Time.deltaTime);
	}
}


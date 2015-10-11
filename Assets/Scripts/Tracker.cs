using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour {

	public Transform m_Target;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = m_Target.position;
	}
}

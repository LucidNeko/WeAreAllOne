using UnityEngine;
using System.Collections;

public class Lifespan : MonoBehaviour
{

	public float m_Duration = 1f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (DestroySelf (m_Duration));
	}
	
	IEnumerator DestroySelf(float delay) {
		yield return new WaitForSeconds(delay);
		Destroy (gameObject);
	}
}


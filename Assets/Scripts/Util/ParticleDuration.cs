using UnityEngine;
using System.Collections;

public class ParticleDuration : MonoBehaviour
{

	public float m_Duration = 1f;
	public float m_Scale = 1f;
	public float m_Speed = 1f;

	void Awake() {
		ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem p in ps) {
			p.startSize = p.startSize * m_Scale;
			p.playbackSpeed = p.playbackSpeed * m_Speed;
		}
	}

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


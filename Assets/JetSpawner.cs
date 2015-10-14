using UnityEngine;
using System.Collections;

public class JetSpawner : MonoBehaviour {

	public GameObject m_Prefab;

	public int m_MaxJets = 5;
	public float m_SpawnDelay = 1f;

	private bool m_CanSpawn = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_CanSpawn && transform.childCount < m_MaxJets) {
			StartCoroutine(Spawn ());
		}
	}

	IEnumerator Spawn() {
		m_CanSpawn = false;
		GameObject plane = Instantiate (m_Prefab, transform.position, transform.rotation) as GameObject;
		plane.transform.parent = transform;
		yield return new WaitForSeconds (m_SpawnDelay);
		m_CanSpawn = true;
	}
}

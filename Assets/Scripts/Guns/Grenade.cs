using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	public float m_Delay = 3f;
	public float m_FragmentSpeed = 20f;
	public int m_NumFragments = 20;

	public GameObject m_Effect;

	public GameObject m_Bullet;

	private PlayerStats m_Stats;

	// Use this for initialization
	void Start () {
		StartCoroutine (Boom ());
	}

	public void SetStats(PlayerStats stats) {
		m_Stats = stats;
	}

	IEnumerator Boom() {
		yield return new WaitForSeconds(m_Delay);

		//Explode effect
		GameObject explosionffect = Instantiate(m_Effect, transform.position, transform.rotation) as GameObject;
		foreach (ParticleSystem ps in explosionffect.GetComponentsInChildren<ParticleSystem>()) {
			ps.startColor = m_Stats.PlayerColor;
		}

		//Shrapnel
		for (int i = 0; i < m_NumFragments; i++) {
			GameObject bullet = Instantiate (m_Bullet, transform.position, Quaternion.identity) as GameObject;
			bullet.transform.parent = TempContainer.Instance.transform; //put in container
			
			bullet.GetComponent<Bullet> ().SetStats (m_Stats);
			
			bullet.GetComponent<Rigidbody> ().AddForce (GetRandomDir() * m_FragmentSpeed, ForceMode.Impulse);
		}

		Destroy (gameObject);
	}

	Vector3 GetRandomDir() {
		return new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized;
	}
}

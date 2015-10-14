using UnityEngine;
using System.Collections;

public class SuicideBomb : MonoBehaviour
{
	public float m_Speed = 6f;

	public float m_FragmentSpeed = 20f;
	public int m_NumFragments = 20;
	
	public float m_DamageRadius = 6f;
	
	public GameObject m_Effect;
	
	public GameObject m_Bullet;
	
	private PlayerStats m_Stats;
	private Transform m_Target;

	private Rigidbody m_Rigidbody;

	void Start() {
		m_Rigidbody = GetComponent<Rigidbody> ();
	}
	
	public void SetTarget(Transform target) {
		m_Target = target;
	}

	public void SetStats(PlayerStats stats) {
		m_Stats = stats;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_Target != null) {
			transform.forward = (m_Target.position - transform.position).normalized;
//			m_Rigidbody.AddForce(transform.forward, ForceMode.Force);
//			transform.position = Vector3.Lerp (transform.position, m_Target.position, Time.deltaTime * m_Speed);
		} else {
			transform.RotateAround (Vector3.zero, Vector3.up, -60 * Time.deltaTime);
		}
	}

	void FixedUpdate() {
		if (m_Target != null) {
			m_Rigidbody.AddForce (transform.forward * 100, ForceMode.Force);
		}
	}

	void OnCollisionEnter(Collision collision) {

		if (m_Target == null) {
			return;
		}

		//Explode effect
		GameObject explosionffect = Instantiate(m_Effect, transform.position, transform.rotation) as GameObject;
		foreach (ParticleSystem ps in explosionffect.GetComponentsInChildren<ParticleSystem>()) {
			ps.startColor = m_Stats.PlayerColor;
		}
		explosionffect.transform.parent = TempContainer.Instance.transform;
		
		//Spherecast
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_DamageRadius);
		foreach (Collider c in colliders) {
			if(c.gameObject.tag.Equals("Player")) {
				HandlePlayerHit(c.gameObject);
			}
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

	void HandlePlayerHit(GameObject obj) {
		if (obj.tag.Equals("Player")) {
			if(obj.GetComponent<PlayerStats>().PlayerColor != m_Stats.PlayerColor) {
				Debug.Log ("Hit player");
				
				obj.GetComponent<PaintableSurface>().Paint(m_Stats, null);
			}
		}
	}
}


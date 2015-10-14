using UnityEngine;
using System.Collections;

public class Shotgun : Gun {
		
	public GameObject m_Bullet;
	public Transform m_BarrelEnd;
	
	public int m_Shots = 10;
	public float m_BulletSpeed = 50f;
	public float m_ShotDelay = 0.5f;
	
	private bool m_Equipped = false;

	private bool m_IsShooting = false;
	
	Camera m_Camera;
	PlayerStats m_PlayerStats;

	private AudioSource m_FireSound;
	
	void Start() {
		m_FireSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Equipped) {
			Debug.DrawRay (m_BarrelEnd.position, GetBulletTrajectory (m_Camera, m_BarrelEnd) * 100);
			
//			if (IsTriggerDown () && !m_IsShooting) {
//				Fire ();
//			}
		}
	}
	
	public override void OnEquip() {
		m_PlayerStats = transform.root.gameObject.GetComponentInChildren<PlayerStats> ();
		m_Camera = transform.root.gameObject.GetComponentInChildren<Camera> ();
		m_Equipped = true;
	}
	
	public override void OnDrop() {
		TriggerState (false);
		m_PlayerStats = null;
		m_Camera = null;
		m_Equipped = false;
		
		PickupManager.Instance.CreatePickup (gameObject);
	}

	protected override void TriggerPressed() {
		if (m_IsShooting) {
			return;
		}

		m_FireSound.Play ();
		
		m_IsShooting = true;
		
		Vector3 dir = GetBulletTrajectory (m_Camera, m_BarrelEnd);
		
		for (int i = 0; i < m_Shots; i++) {
			GameObject bullet = Instantiate (m_Bullet, m_BarrelEnd.position, m_BarrelEnd.rotation) as GameObject;
			bullet.transform.parent = TempContainer.Instance.transform; //put in container
			
			bullet.GetComponent<Bullet> ().SetStats (m_PlayerStats);
			
			Vector3 randomDir = dir + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
			
			bullet.GetComponent<Rigidbody> ().AddForce (randomDir.normalized * m_BulletSpeed, ForceMode.Impulse);
		}
		
		StartCoroutine (Delay ());
	}

	private IEnumerator Delay() {
		yield return new WaitForSeconds (m_ShotDelay);
		m_IsShooting = false;
	}
	
//	private void Fire() {
//		StartCoroutine (FireRoutine ());
//	}
//	
//	private IEnumerator FireRoutine() {
////		m_FireSound.Play ();
////
////		m_IsShooting = true;
////
////		Vector3 dir = GetBulletTrajectory (m_Camera, m_BarrelEnd);
////
////		for (int i = 0; i < m_Shots; i++) {
////			GameObject bullet = Instantiate (m_Bullet, m_BarrelEnd.position, m_BarrelEnd.rotation) as GameObject;
////			bullet.transform.parent = TempContainer.Instance.transform; //put in container
////			
////			bullet.GetComponent<Bullet> ().SetStats (m_PlayerStats);
////
////			Vector3 randomDir = dir + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
////			
////			bullet.GetComponent<Rigidbody> ().AddForce (randomDir.normalized * m_BulletSpeed, ForceMode.Impulse);
////		}
////
////		yield return new WaitForSeconds (m_ShotDelay);
////		m_IsShooting = false;
//	}

	public override string GetName() {
		return "Shotgun";
	}
}

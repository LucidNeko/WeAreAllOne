using UnityEngine;
using System.Collections;

public class Pistol : Gun {
		
	public GameObject m_Bullet;
	public Transform m_BarrelEnd;

	public float m_BulletSpeed = 50f;
	
	private bool m_Equipped = false;
	
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
		m_FireSound.Play ();

		Vector3 dir = GetBulletTrajectory (m_Camera, m_BarrelEnd);
		
		GameObject bullet = Instantiate (m_Bullet, m_BarrelEnd.position, m_BarrelEnd.rotation) as GameObject;
		bullet.transform.parent = TempContainer.Instance.transform; //put in container
		
		bullet.GetComponent<Bullet> ().SetStats (m_PlayerStats);
		
		bullet.GetComponent<Rigidbody> ().AddForce (dir * m_BulletSpeed, ForceMode.Impulse);
	}

	public override string GetName() {
		return "Pistol";
	}
}

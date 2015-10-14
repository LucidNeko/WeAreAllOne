using UnityEngine;
using System.Collections;

public class BurstRifle : Gun {

	public GameObject m_Bullet;

	public float m_BulletSpeed = 30f;

	public Transform m_BarrelEnd;

	private PlayerStats m_PlayerStats;
	private Camera m_Camera;

	private bool m_CanShoot = true;
	private int m_MaxShots = 3;
	private int m_ShotsFired = 0;

	private bool m_Equipped = false;

	private AudioSource m_FireSound;

	void Start() {
		m_FireSound = GetComponent<AudioSource> ();
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
	
	// Update is called once per frame
	void Update () {
		if (m_Equipped) {
			Debug.DrawRay (m_BarrelEnd.position, GetBulletTrajectory (m_Camera, m_BarrelEnd) * 100);

			if (IsTriggerDown () && m_CanShoot && m_ShotsFired < m_MaxShots) {
				Fire ();
			}
		}
	}

	protected override void TriggerReleased() {
		m_ShotsFired = 0;
	}

	public void Fire() {
		StartCoroutine (FireRoutine ());
	}

	private IEnumerator FireRoutine() {
		m_FireSound.Play ();

		m_CanShoot = false;
		GameObject bullet = Instantiate (m_Bullet, m_BarrelEnd.position, m_BarrelEnd.rotation) as GameObject;
		bullet.transform.parent = TempContainer.Instance.transform; //put in container

		bullet.GetComponent<Bullet> ().SetStats (m_PlayerStats);
		
		bullet.GetComponent<Rigidbody> ().AddForce (GetBulletTrajectory (m_Camera, m_BarrelEnd) * m_BulletSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(0.05f);
		m_ShotsFired++;
		m_CanShoot = true;
	}

	public override string GetName() {
		return "Burst Rifle";
	}
}

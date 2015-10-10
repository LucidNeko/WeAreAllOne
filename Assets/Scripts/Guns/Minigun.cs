using UnityEngine;
using System.Collections;

public class Minigun : Gun {

	public GameObject m_Bullet;
	public Transform m_Barrel;
	public Transform m_BarrelEnd;

	public int m_MaxShots = 30;
	public float m_WarmupDelay = 0.7f;
	public float m_BulletSpeed = 50f;
	public float m_ShotDelay = 0.02f;
	public float m_CooldownDuration = 1f;

	private bool m_Equipped = false;

	private bool m_IsCoolingDown = false;
	private bool m_IsShooting = false;
	private int m_ShotsFired = 0;

	Camera m_Camera;
	PlayerStats m_PlayerStats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Equipped) {
			Debug.DrawRay (m_BarrelEnd.position, GetBulletTrajectory (m_Camera, m_BarrelEnd) * 100);
			
			if (IsTriggerDown () && !m_IsCoolingDown && !m_IsShooting && m_ShotsFired < m_MaxShots) {
				Fire ();
			}

			if(IsTriggerDown() && !m_IsCoolingDown && m_ShotsFired < m_MaxShots) {
				Vector3 rot = m_Barrel.localEulerAngles;
				rot.z += 10f;
				m_Barrel.localEulerAngles = rot;
			}
		}
	}

	protected override void TriggerReleased() {
		if (!m_IsCoolingDown) {
			StartCoroutine (Cooldown ());
		}
	}

	IEnumerator Cooldown() {
		m_IsCoolingDown = true;
		yield return new WaitForSeconds (m_CooldownDuration);
		m_IsCoolingDown = false;
		m_ShotsFired = 0;
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

	private void Fire() {
		StartCoroutine (FireRoutine ());
	}

	private IEnumerator FireRoutine() {
		m_IsShooting = true;

		GameObject bullet = Instantiate (m_Bullet, m_BarrelEnd.position, m_BarrelEnd.rotation) as GameObject;
		bullet.transform.parent = TempContainer.Instance.transform; //put in container
		
		bullet.GetComponent<Bullet> ().SetStats (m_PlayerStats);
		
		bullet.GetComponent<Rigidbody> ().AddForce (GetBulletTrajectory (m_Camera, m_BarrelEnd) * m_BulletSpeed, ForceMode.Impulse);

		m_ShotsFired++;
		yield return new WaitForSeconds (m_ShotDelay);
		m_IsShooting = false;
	}

	public override string GetName() {
		return "Minigun";
	}
}

using UnityEngine;
using System.Collections;

public class BurstRifle : Gun {

	public GameObject m_Bullet;

	public float m_BulletSpeed = 30f;

	public Transform m_BarrelEnd;

	private GameObject m_Bullets;

	private PlayerStats m_PlayerStats;
	private Camera m_Camera;

	private bool m_CanShoot = true;
	private int m_MaxShots = 3;
	private int m_ShotsFired = 0;

	private bool m_Equipped = false;
	
	// Use this for initialization
	void Start () {
//		m_PlayerStats = GetComponentInParent<PlayerStats> ();
//		m_Camera = GetComponentInParent<Camera> ();
//		Equip (transform.root.gameObject);
	}

	public override void Equip(GameObject player) {
		player.GetComponentInParent<GunScript> ().Equip (this);

		m_PlayerStats = player.GetComponentInChildren<PlayerStats> ();
		m_Camera = player.GetComponentInChildren<Camera> ();
		m_Equipped = true;
	}

	public override void Drop() {
		transform.parent = null;
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

	void OnEnable() {
		//Make container to store bullets
		m_Bullets = new GameObject ("Bullet Container");
	}

	void OnDisable() {
		Destroy (m_Bullets);
	}

	protected override void TriggerReleased() {
		m_ShotsFired = 0;
	}

	public void Fire() {
		StartCoroutine (FireRoutine ());
	}

	private IEnumerator FireRoutine() {
		m_CanShoot = false;
		GameObject bullet = Instantiate (m_Bullet, m_BarrelEnd.position, m_BarrelEnd.rotation) as GameObject;
		bullet.transform.parent = m_Bullets.transform; //put in container
		bullet.GetComponent<Bullet> ().SetColor (m_PlayerStats.PlayerColor);
		
		bullet.GetComponent<Rigidbody> ().AddForce (GetBulletTrajectory (m_Camera, m_BarrelEnd) * m_BulletSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(0.05f);
		m_ShotsFired++;
		m_CanShoot = true;
	}
}

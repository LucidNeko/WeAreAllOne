using UnityEngine;
using System.Collections;

public class Minigun : Gun {

	public GameObject m_Bullet;
	public Transform m_Barrel;
	public Transform m_BarrelEnd;

	public int m_MaxShots = 50;
	public float m_BulletSpeed = 50f;
	public float m_ShotDelay = 0.02f;

	private bool m_Equipped = false;

	private bool m_IsShooting = false;

	private float m_SpinSpeed = 0;
	private float m_MaxSpinSpeed = 1000f;

	private float m_Heat = 0;
	private float m_MaxHeat = 100; //num shots
	private float m_HeatPer = 1f;

	private bool m_Overheat = false;

	Camera m_Camera;
	PlayerStats m_PlayerStats;

	// Use this for initialization
	void Start () { 
		m_MaxHeat = m_MaxShots;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Equipped) {
			Debug.DrawRay (m_BarrelEnd.position, GetBulletTrajectory (m_Camera, m_BarrelEnd) * 100);
			
			//rotate
			m_Barrel.Rotate(Vector3.forward, m_SpinSpeed * Time.deltaTime);

			//calc spin
			if(IsTriggerDown() && !m_Overheat) {
				m_SpinSpeed += 10;
			} else {
				m_SpinSpeed -= 10;
				m_Heat -= m_HeatPer;
			}
			m_SpinSpeed = Mathf.Clamp(m_SpinSpeed, 0, m_MaxSpinSpeed);

			//calc heat
			if(m_Heat > m_MaxHeat) {
				m_Heat = m_MaxHeat;
			} else if( m_Heat < 0 && !IsTriggerDown()) { //resets on trigger release and 0 heat
				m_Heat = 0;
				m_Overheat = false;
			}

			//overheat?
			if(m_Heat == m_MaxHeat) {
				m_Overheat = true;
			}

			//cooldown?
			if(m_Overheat) {
				m_Heat -= 0.5f;
			} else {
				//fire
				if(IsTriggerDown() && !m_IsShooting && m_SpinSpeed == m_MaxSpinSpeed) {
					Fire ();
					m_Heat += m_HeatPer;
				}
			}
		}
	}

	protected override void TriggerReleased() {
	
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

		yield return new WaitForSeconds (m_ShotDelay);
		m_IsShooting = false;
	}

	public override string GetName() {
		return "Minigun";
	}
}

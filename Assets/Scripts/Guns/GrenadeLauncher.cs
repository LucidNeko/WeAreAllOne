using UnityEngine;
using System.Collections;

public class GrenadeLauncher : Gun
{

	public GameObject m_Grenade;
	public Transform m_BarrelEnd;

	public float m_ShotDelay = 1f;
	public float m_ShotSpeed = 4f;

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
			
			if (IsTriggerDown () && !m_IsShooting) {
				Fire ();
			}
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

	private void Fire() {
		StartCoroutine (FireRoutine ());
	}
	
	private IEnumerator FireRoutine() {
		m_FireSound.Play ();

		m_IsShooting = true;
		
		Vector3 dir = GetBulletTrajectory (m_Camera, m_BarrelEnd);

		GameObject grenade = Instantiate (m_Grenade, m_BarrelEnd.position, m_BarrelEnd.rotation) as GameObject;
		grenade.transform.parent = TempContainer.Instance.transform; //put in container
		
		grenade.GetComponent<Grenade> ().SetStats (m_PlayerStats);

		grenade.GetComponent<Rigidbody> ().AddForce (dir * m_ShotSpeed, ForceMode.Impulse);
		
		yield return new WaitForSeconds (m_ShotDelay);
		m_IsShooting = false;
	}
	
	public override string GetName() {
		return "Grenade Launcher";
	}
}


using UnityEngine;
using System.Collections;

public class WalkieTalkie : Gun {
		
	public Transform m_BarrelEnd;

	public GameObject m_SuicideBomber;
	
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

		if (transform.localRotation == Quaternion.identity) {
			transform.localRotation = Quaternion.Euler(-7, 215, -18);
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
//		m_FireSound.Play ();

		GameObject target = GetTargetGameObject(m_Camera, m_BarrelEnd);

		if (target == null) {
			return;
		}

//		GameObject plane = GameObject.FindGameObjectWithTag ("Jet Suicide") as GameObject;
		GameObject[] planes = GameObject.FindGameObjectsWithTag ("Jet Suicide");



//		if (plane == null) {
//			return;
//		}

		if (planes.Length == 0) {
			return;
		}

		GameObject plane = planes [Random.Range (0, planes.Length)];

		SuicideBomb script = plane.GetComponent<SuicideBomb> ();

		script.SetStats (m_PlayerStats);
		script.SetTarget (target.transform);
	}

	public override string GetName() {
		return "Pistol";
	}
}

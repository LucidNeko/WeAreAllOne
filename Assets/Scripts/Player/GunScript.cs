using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	public GameObject m_GunSlot;

	private IControl m_Control;

	// Use this for initialization
	void Start () {
		m_Control = GetComponent<IControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		Gun gun = m_GunSlot.GetComponentInChildren<Gun> ();
		if (gun != null) {
			gun.TriggerState(m_Control.GetShoot());
		}
	}

	public void Equip(Gun gun) {
		Gun oldGun = m_GunSlot.GetComponentInChildren<Gun> ();
		if (oldGun != null) {
			oldGun.Drop();
		}

		GetComponent<PlayerStats> ().SetGunRenderer (gun.gameObject.GetComponent<Renderer> ());

		gun.transform.parent = m_GunSlot.transform;
		gun.transform.localPosition = Vector3.zero;
		gun.transform.localRotation = Quaternion.identity;
	}
}

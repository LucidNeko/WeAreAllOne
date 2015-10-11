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

	public void Drop() {
		Gun oldGun = m_GunSlot.GetComponentInChildren<Gun> ();
		if (oldGun != null) {
			PlayerStats stats = GetComponent<PlayerStats> ();
			Renderer[] renderers = oldGun.GetComponentsInChildren<Renderer>();
			foreach(Renderer renderer in renderers) {
				stats.FreeMaterial(renderer);
			}
			oldGun.transform.parent = null;
			oldGun.OnDrop();
		}
	}

	public void Equip(IEquippable item) {
		if (item is Gun) {
			Equip ((Gun)item);
		}
	}

	public void Equip(Gun gun) {
		Drop ();

		PlayerStats stats = GetComponent<PlayerStats> ();
		Renderer[] renderers = gun.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in renderers) {
			stats.HijackMaterial(renderer);
		}

		gun.transform.parent = m_GunSlot.transform;
		gun.transform.localPosition = gun.m_Offset;
		gun.transform.localRotation = Quaternion.identity;

		gun.OnEquip ();
	}
}

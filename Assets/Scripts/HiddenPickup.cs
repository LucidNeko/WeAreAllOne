using UnityEngine;
using System.Collections;

public class HiddenPickup : MonoBehaviour
{
	public GameObject m_GunPrefab;

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag.Equals ("Player")) {
			Gun gun = Instantiate(m_GunPrefab).GetComponent<Gun>();
			Gun oldGun = collider.gameObject.GetComponentInChildren<Gun>();
			if(oldGun != null) {
				if(gun.GetName().Equals(oldGun.GetName ())) {
					Destroy(gun.gameObject);
					return;
				}
			}
			collider.gameObject.GetComponent<GunScript>().Equip(gun);
		}
	}
}


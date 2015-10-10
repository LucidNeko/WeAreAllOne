using UnityEngine;
using System.Collections;

public class PickupOrb : MonoBehaviour
{
	public GameObject m_Object;

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 90 * Time.deltaTime, 0);
	}
	
	void OnTriggerEnter(Collider collider) {
		if (m_Object == null) {
			return;
		}
		
		if (collider.gameObject.tag.Equals ("Player")) {
			GunScript gs = collider.gameObject.GetComponent<GunScript>();
			IEquippable e = m_Object.GetComponent<IEquippable>();
			if(gs != null && e != null) {
				gs.Equip(e);
				m_Object = null;
				Destroy (gameObject);
			}
		}
	}
}


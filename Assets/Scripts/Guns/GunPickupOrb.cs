using UnityEngine;
using System.Collections;

public class GunPickupOrb : MonoBehaviour {

	public GameObject m_Gun;

	void Start() {
		m_Gun = Instantiate (m_Gun) as GameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		m_Gun.transform.localPosition = Vector3.zero;
		m_Gun.transform.up = Vector3.up;
	}

	void OnCollisionEnter(Collision collision) {
		if (m_Gun == null) {
			return;
		}

		if (collision.gameObject.tag.Equals ("Player")) {
			GunScript gs = collision.gameObject.GetComponent<GunScript>();
			gs.Equip(m_Gun.GetComponent<Gun>());
			m_Gun = null;
			Destroy (gameObject);
		}
	}
}

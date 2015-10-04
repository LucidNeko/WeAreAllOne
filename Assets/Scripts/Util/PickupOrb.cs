using UnityEngine;
using System.Collections;

public class PickupOrb : MonoBehaviour
{
	public GameObject m_Object;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 90 * Time.deltaTime, 0);
	}
	
	void OnTriggerEnter(Collider collider) {
		if (m_Object == null) {
			return;
		}
		
		if (collider.gameObject.tag.Equals ("Player")) {
			IEquippable e = m_Object.GetComponent<IEquippable>();
			if(e != null) {
				e.Equip(collider.gameObject);
				m_Object = null;
				Destroy (gameObject);
			}
		}
	}
}


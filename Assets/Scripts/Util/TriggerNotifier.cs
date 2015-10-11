using UnityEngine;
using System.Collections;

public class TriggerNotifier : MonoBehaviour {

	public delegate void TriggerDelegate(bool state, Collider other);
	public event TriggerDelegate OnTrigger;

	void OnTriggerEnter(Collider other) {
		OnTrigger (true, other);
	}

	//TODO: this makes us have 3 in air manouvers..
	//because it resets moves remaining while you're jumping off ground
	void OnTriggerStay(Collider other) {
		OnTrigger (true, other);
	}

	void OnTriggerExit(Collider other) {
		OnTrigger (false, other);
	}
}

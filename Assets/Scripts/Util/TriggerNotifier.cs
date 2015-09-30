using UnityEngine;
using System.Collections;

public class TriggerNotifier : MonoBehaviour {

	public delegate void TriggerDelegate(bool state, Collider other);
	public event TriggerDelegate OnTrigger;

	void OnTriggerEnter(Collider other) {
		OnTrigger (true, other);
	}

	void OnTriggerExit(Collider other) {
		OnTrigger (false, other);
	}
}

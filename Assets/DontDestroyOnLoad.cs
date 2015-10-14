using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	public static DontDestroyOnLoad Instance {
		get;
		private set;
	}

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}
		
		Instance = this;

		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

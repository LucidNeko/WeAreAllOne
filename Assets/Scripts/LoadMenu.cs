using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

			Application.LoadLevel(0);
		}
	}

}

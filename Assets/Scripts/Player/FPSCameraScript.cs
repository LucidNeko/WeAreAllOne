using UnityEngine;
using System.Collections;

public class FPSCameraScript : MonoBehaviour {

	public Texture2D m_Crosshair;
	public bool m_InvertX = false;
	public bool m_InvertY = false;
	public float m_SpeedX = 6f;
	public float m_SpeedY = 6f;

	private IControl m_Control;
	private Camera m_Camera;

	void Start() {
		m_Control = GetComponentInParent<IControl> ();
		m_Camera = GetComponent<Camera> ();

		//initially grab the mouse
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		if (m_Control.GetInvertDown ()) {
			m_InvertY = !m_InvertY;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float dx = m_Control.GetHorizontalAxis2() * m_SpeedX;
		float dy = m_Control.GetVerticalAxis2() * m_SpeedY;
		if(m_InvertX) { dx = -dx; }
		if(m_InvertY) { dy = -dy; }

		transform.root.Rotate (0, dx, 0, Space.World);
		transform.Rotate (dy, 0, 0, Space.Self);

		//clamp in range so can't look full 360
		Vector3 v = transform.localEulerAngles;
		if (v.y > 90) {
			if(v.x > 20 && v.x < 120) {
				v.x = 90;
			} else {
				v.x = 270;
			}
			v.y = 0; v.z = 0;
		}
		transform.localEulerAngles = v;
	}
	
	void OnGUI() {
		Vector2 center = m_Camera.pixelRect.center;
		center.y = Screen.height - center.y; //screen to GUI coords

		Vector2 cross = new Vector2(m_Crosshair.width, m_Crosshair.height);
		cross *= 0.5f; //scale crosshair

		GUI.DrawTexture (new Rect (center.x - (cross.x/2f), (center.y - (cross.y/2f)), cross.x, cross.y), m_Crosshair);
	}
}

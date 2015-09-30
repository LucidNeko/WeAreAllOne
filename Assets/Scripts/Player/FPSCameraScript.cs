using UnityEngine;
using System.Collections;

public class FPSCameraScript : MonoBehaviour {

	public Texture2D m_Crosshair;
	public bool m_InvertX = false;
	public bool m_InvertY = true;
	public float m_SpeedX = 6f;
	public float m_SpeedY = 6f;

	private IControl m_Control;
	private Camera m_Camera;

	void Start() {
		m_Control = GetComponentInParent<IControl> ();
		m_Camera = GetComponent<Camera> ();
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
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
	}

	void OnGUI() {
		Vector2 center = m_Camera.pixelRect.center;
		center.y = Screen.height - center.y; //screen to GUI coords

		Vector2 cross = new Vector2(m_Crosshair.width, m_Crosshair.height);
		cross *= 0.5f; //scale crosshair

		GUI.DrawTexture (new Rect (center.x - (cross.x/2f), (center.y - (cross.y/2f)), cross.x, cross.y), m_Crosshair);
	}
}

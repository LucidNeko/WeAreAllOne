using UnityEngine;
using System.Collections;

public class FPSCameraScript : MonoBehaviour {

	public Texture2D m_Logo;

	public Transform m_Target;
	public Texture2D m_Crosshair;
	public bool m_InvertX = false;
	public bool m_InvertY = true;
	public float m_SpeedX = 6f;
	public float m_SpeedY = 6f;

	private Camera m_Camera;

	void Start() {
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
		transform.position = m_Target.position;

		float dx = Input.GetAxis ("Mouse X") * m_SpeedX;
		float dy = Input.GetAxis ("Mouse Y") * m_SpeedY;
		if(m_InvertX) { dx = -dx; }
		if(m_InvertY) { dy = -dy; }

		transform.Rotate (0, dx, 0, Space.World);
		transform.Rotate (dy, 0, 0, Space.Self);
	}

	void OnGUI() {
		Vector2 center = m_Camera.pixelRect.center;
		center.y = Screen.height - center.y; //screen to GUI coords

		Vector2 cross = new Vector2(m_Crosshair.width, m_Crosshair.height);
		cross *= 0.5f; //scale crosshair

		GUI.DrawTexture (new Rect (center.x - (cross.x/2f), (center.y - (cross.y/2f)), cross.x, cross.y), m_Crosshair);

		Rect logoRect = new Rect();
		logoRect.width = m_Logo.width;
		logoRect.height = m_Logo.height;

		GUI.DrawTexture (logoRect, m_Logo);
	}
}

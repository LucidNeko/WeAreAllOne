using UnityEngine;
using System.Collections;

public class KeyboardControl : MonoBehaviour, IControl
{
	public static readonly string HORIZONTAL_AXIS = "Horizontal Keyboard";
	public static readonly string VERTICAL_AXIS = "Vertical Keyboard";

	public static readonly string HORIZONTAL_AXIS2 = "Mouse X";
	public static readonly string VERTICAL_AXIS2 = "Mouse Y";

	public static readonly string JUMP = "Jump Keyboard";
	public static readonly int SHOOT = 0;

	//dash
	public float m_DashThreshold = 0.8f;
	public float m_DashRestThreshold = 0.5f;
	public float m_DashWindow = 0.2f;
	private bool m_DashListening = false;

	public event Dash OnDash;

	public float GetHorizontalAxis () {
		return Input.GetAxisRaw (HORIZONTAL_AXIS);
	}

	public float GetVerticalAxis () {
		return Input.GetAxisRaw (VERTICAL_AXIS);
	}

	public float GetHorizontalAxis2 () {
		return Input.GetAxis (HORIZONTAL_AXIS2);
	}

	public float GetVerticalAxis2 () {
		return Input.GetAxis (VERTICAL_AXIS2);
	}

	public bool GetJump() {
		return Input.GetButton (JUMP);
	}

	public bool GetJumpDown() {
		return Input.GetButtonDown (JUMP);
	}
	
	public bool GetShoot() {
		return Input.GetMouseButton (SHOOT);
	}

	public bool GetShootDown() {
		return Input.GetMouseButtonDown (SHOOT);
	}

	public float GetShootTrigger () {
		return Input.GetMouseButton (SHOOT) ? 1 : 0;
	}

	public bool GetInvertDown() {
		return Input.GetKeyDown (KeyCode.I);
	}

	void Update() {
		float v = GetVerticalAxis ();
		float h = GetHorizontalAxis ();

		if (SqrMag(v, h) > m_DashThreshold) {
			if(!m_DashListening) {
				m_DashListening = true;
				StartCoroutine(ListenForDash());
			}
		}
	}

	IEnumerator ListenForDash() {
		bool restPosition = false;

		float t = 0;
		while (t < m_DashWindow) {
			float v = GetVerticalAxis ();
			float h = GetHorizontalAxis ();

			if(!restPosition && SqrMag(v, h) < m_DashRestThreshold) {
				restPosition = true;
			} else if(restPosition && SqrMag(v, h) > m_DashThreshold) {
				if(OnDash != null) {
					float invScale = 1f/Mathf.Sqrt(SqrMag (v, h)); //normalize
					OnDash(v*invScale, h*invScale);
				}
				break;
			}

			t += Time.deltaTime;
			yield return null;
		}

		m_DashListening = false;
	}

	private float SqrMag(float x, float y) { return x * x + y * y; }
}


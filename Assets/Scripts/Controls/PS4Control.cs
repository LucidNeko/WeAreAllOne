using UnityEngine;
using System.Collections;

public class PS4Control : MonoBehaviour, IControl
{

	public int m_Player = 0; //number of controller (0~3 inclusive)

	//dash
	public float m_DashThreshold = 0.8f;
	public float m_DashRestThreshold = 0.5f;
	public float m_DashWindow = 0.2f;
	private bool m_DashListening = false;

	public event Dash OnDash;
	
	public float GetHorizontalAxis ()
	{
		return Input.GetAxis("PS4 Horizontal P" + m_Player);
	}
	
	public float GetVerticalAxis ()
	{
		return Input.GetAxis("PS4 Vertical P" + m_Player);
	}
	
	public float GetHorizontalAxis2 ()
	{
		return Input.GetAxis("PS4 Horizontal2 P" + m_Player);
	}
	
	public float GetVerticalAxis2 ()
	{
#if UNITY_EDITOR_WIN
		return Input.GetAxis("PS4 Win Vertical2 P" + m_Player);
#elif UNITY_STANDALONE_WIN
		return Input.GetAxis("PS4 Win Vertical2 P" + m_Player);
#else
		return Input.GetAxis("PS4 Vertical2 P" + m_Player);
#endif
	}
	
	public bool GetJump ()
	{
		return Input.GetButton ("PS4 Jump P" + m_Player);
	}
	
	public bool GetJumpDown ()
	{
		return Input.GetButtonDown ("PS4 Jump P" + m_Player);
	}
	
	public bool GetShoot ()
	{
		return Input.GetButton ("PS4 Shoot P" + m_Player);
	}
	
	public bool GetShootDown ()
	{
		return Input.GetButtonDown ("PS4 Shoot P" + m_Player);
	}

	public float GetShootTrigger () {
		return Input.GetAxisRaw ("PS4 Shoot Trigger P" + m_Player);
	}

	public bool GetInvertDown() {
		return Input.GetButtonDown ("PS4 Invert P" + m_Player);
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


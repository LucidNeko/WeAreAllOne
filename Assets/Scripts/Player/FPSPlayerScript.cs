using UnityEngine;
using System.Collections;

public class FPSPlayerScript : MonoBehaviour {

	private static readonly Vector3 XZ_PLANE = new Vector3(1, 0, 1);

	private PlayerController m_PlayerController;
	private Transform m_Camera;
	private IControl m_Control;
	private Animator m_Animator;

	private Vector3 move = Vector3.zero;
	private bool jump = false;

	// Use this for initialization
	void Start () {
		m_Control = GetComponent<IControl> ();
		m_PlayerController = GetComponent<PlayerController> ();
		m_Camera = GetComponentInChildren<Camera> ().transform;
//		m_Camera = LevelManager.Instance.GetCamera (gameObject).transform;
		m_Animator = GetComponent<Animator> ();
	}

	void Update() {
		jump = jump ? jump : m_Control.GetJumpDown(); //if jump not applied yet, leave it alone

		float h = m_Control.GetHorizontalAxis();
		float v = m_Control.GetVerticalAxis();

		move.x = h;
		move.z = v;

		if (h != 0 || v != 0) {
			Quaternion q = Quaternion.LookRotation (move);
			float rot = q.eulerAngles.y;
			if (rot > 180) {
				rot -= 360;
			}
			
			m_Animator.SetFloat ("InputMagnitude", move.magnitude);
			m_Animator.SetFloat ("InputDirection", rot);
		} else {
			m_Animator.SetFloat ("InputMagnitude", 0);
			m_Animator.SetFloat ("InputDirection", 0);
		}

		Vector3 forward = m_Camera.forward;
		
		forward = Vector3.Scale(forward, XZ_PLANE).normalized;
		move = v * forward + h * m_Camera.right;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		m_PlayerController.Move(move, jump);
		jump = false;
	}
}

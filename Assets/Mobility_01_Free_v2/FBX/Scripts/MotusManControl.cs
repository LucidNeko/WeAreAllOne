using UnityEngine;
using System.Collections;

public class MotusManControl : MonoBehaviour {

	public Transform m_LeftFoot;
	public Transform m_RightFoot;

	private Animator m_Animator;
	private Rigidbody m_RigidBody;
	private Transform m_Camera;

	void Start() {
		m_Animator = GetComponent<Animator> ();
		m_RigidBody = GetComponent<Rigidbody> ();
		m_Camera = GetComponentInChildren<Camera> ().transform;

		m_Animator.speed = 1.5f;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (h != 0 || v != 0) {
			Vector3 move = new Vector3 (h, 0, v).normalized;
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

		m_Animator.SetBool ("IsRightLegUp", m_RightFoot.position.y > m_LeftFoot.position.y);



		float mx = Input.GetAxis ("Mouse X");
		float my = Input.GetAxis ("Mouse Y");

		transform.Rotate (0, mx, 0, Space.World);
		transform.Rotate (my, 0, 0, Space.Self);
	}
}

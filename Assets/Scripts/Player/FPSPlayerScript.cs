using UnityEngine;
using System.Collections;

public class FPSPlayerScript : MonoBehaviour {

	private static Vector3 XZ_PLANE = new Vector3(1, 0, 1);

	private PlayerController m_PlayerController;
	private Transform m_Camera;

	private Animator m_Animator;

	// Use this for initialization
	void Start () {
		m_PlayerController = GetComponent<PlayerController> ();

		m_Camera = GameObject.FindGameObjectWithTag ("Player 1 Camera").transform;

		m_Animator = GetComponent<Animator> ();
		m_Animator.speed = 1.5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		bool jump = Input.GetButton ("Jump");

		Vector3 forward = Vector3.Scale(m_Camera.forward, XZ_PLANE).normalized;
		Vector3 move = v * forward + h * m_Camera.right;

		m_PlayerController.Move(move, jump);

		if (Input.GetMouseButton (0)) {
			m_PlayerController.Fire();
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)) {
			GetComponent<Animator>().speed = 2f;
		}

		if(Input.GetKeyUp(KeyCode.LeftShift)) {
			GetComponent<Animator>().speed = 1.5f;
		}

		//if fall off bottom of the world reset position
		if(transform.position.y < -20f) {
			transform.position = Vector3.up * 30f;
		}
	}
}

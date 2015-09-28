using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public int m_MaxAirManeuvers = 1; //effectively get 1 extra manouver because resets while on ground

	public float m_Speed = 6f;
	public float m_InAirGravityMultiplier = 3f;

	private IControl m_Control;

	private Transform m_Camera;
	private Rigidbody m_RigidBody;
	private Animator m_Animator;

	private int m_AirManeuversRemaining;
	private bool m_IsGrounded = true;

	private Vector3 dash = Vector3.zero;

	private Transform m_Feet;


	//Awake is where you initialize yourself, but you don't speak with anyone else. 
	//In start, you know that everybody has woken up, so you can speak with them there

	void Awake() {
		m_RigidBody = GetComponent<Rigidbody> ();
		m_Animator = GetComponent<Animator> ();

		
		m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | 
								  RigidbodyConstraints.FreezeRotationY | 
								  RigidbodyConstraints.FreezeRotationZ;

		
		m_AirManeuversRemaining = m_MaxAirManeuvers;
	}

	void Start() {
//		m_Camera = GetComponentInChildren<Camera> ().transform;
		m_Camera = LevelManager.Instance.GetCamera (gameObject).transform;

		//do it in start for first time, as m_Control doesn't exist yet for OnEnable
		m_Control = GetComponent<IControl> ();
		m_Control.OnDash += OnDash;

		m_Feet = transform.FindChild ("Feet");
	}

	//Awake
	//OnEnable
	//Start
	//OnDisable

	void OnEnable() {
		transform.FindChild ("Feet").gameObject.GetComponent<TriggerNotifier> ().OnTrigger += SetGroundStatus;
		if (m_Control != null) {
			m_Control.OnDash += OnDash;
		}
	}

	void OnDisable() {
		transform.FindChild ("Feet").gameObject.GetComponent<TriggerNotifier> ().OnTrigger -= SetGroundStatus;
		m_Control.OnDash -= OnDash;
	}

	public void Move(Vector3 move, bool jump) {
		if (m_IsGrounded) {
			m_AirManeuversRemaining = m_MaxAirManeuvers;
		}

		//If we're moving
		if (move.x != 0 || move.y != 0 || move.z != 0) {
			//look in move direction
			Quaternion look = Quaternion.LookRotation(move);
			m_RigidBody.MoveRotation(look);
		}

		if (m_AirManeuversRemaining > 0) {
			Vector3 force = Vector3.zero;
			if(jump && m_AirManeuversRemaining > 0) {
//				force += Vector3.up * 20f;
//				m_RigidBody.AddForce(Vector3.up * 20f, ForceMode.VelocityChange);
				Vector3 v = m_RigidBody.velocity;
				v.y = 20f;
				m_RigidBody.velocity = v;
				m_AirManeuversRemaining--;
			}
			if(dash != Vector3.zero && m_AirManeuversRemaining > 0) {
//				force += dash * 50f;
//				m_RigidBody.AddForce(dash*50f, ForceMode.Impulse);
				//dash animation
				Debug.Log ("Dash");
				dash = Vector3.zero;
				m_AirManeuversRemaining--;
			}
//			if(force != Vector3.zero) {
//				m_RigidBody.AddForce(force, ForceMode.Impulse);
//			}
		}

		if (!m_IsGrounded) {
			//fall faster when in air
			m_RigidBody.AddForce ((Physics.gravity * m_InAirGravityMultiplier) - Physics.gravity);
		}

		//Set animator properties
		m_Animator.SetFloat ("Speed", move.magnitude * m_Speed);
	}

	private void SetGroundStatus(bool state, Collider other) {
//		m_IsGrounded = state;
//
//		if (m_IsGrounded) {
//			m_AirManeuversRemaining = m_MaxAirManeuvers;
//		}
	}

	private void SetGroundStatus() {
		Ray ray = new Ray (m_Feet.position, Vector3.down);
		RaycastHit info;
		if (Physics.SphereCast (ray, 0.1f, out info, 1f)) {
			m_IsGrounded = true;
			m_AirManeuversRemaining = m_MaxAirManeuvers;
		} else {
			m_IsGrounded = false;
		}
	}

	private void OnDash(float v, float h) {
		Vector3 forward = Vector3.Scale(m_Camera.forward, new Vector3(1,0,1)).normalized;
		this.dash = v * forward + h * m_Camera.right;
	}

}


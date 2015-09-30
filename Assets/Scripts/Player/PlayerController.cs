using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public Transform m_LeftFoot;
	public Transform m_RightFoot;

	public float m_JumpPower = 8f;
	public float m_AirSpeed = 3f;

	public int m_MaxAirManeuvers = 2;

	public float m_Speed = 4f;
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
		m_Animator.speed = m_Speed;

		
		m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | 
								  RigidbodyConstraints.FreezeRotationY | 
								  RigidbodyConstraints.FreezeRotationZ;

		
		m_AirManeuversRemaining = m_MaxAirManeuvers;
	}

	void Start() {
		m_Camera = GetComponentInChildren<Camera> ().transform;
//		m_Camera = LevelManager.Instance.GetCamera (gameObject).transform;

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
		if (m_AirManeuversRemaining > 0) {
			Vector3 force = Vector3.zero;
			if(jump && m_AirManeuversRemaining > 0) {
				Vector3 v = m_RigidBody.velocity;
				v.y = m_JumpPower;
				m_RigidBody.velocity = v;
				m_AirManeuversRemaining--;
			}
			if(!m_IsGrounded && dash != Vector3.zero && m_AirManeuversRemaining > 0) {
				//dash animation
				Debug.Log ("Dash");
				dash = Vector3.zero;
				m_AirManeuversRemaining--;
			}
		}

//		if (move.x != 0 || move.y != 0 || move.z != 0) {
//			if(!m_IsGrounded) {
//				//in air
//				Vector3 v = m_RigidBody.velocity;
//				v.x = move.x * m_AirSpeed;
//				v.z = move.z * m_AirSpeed;
//				m_RigidBody.velocity = v;
//			}
//		}

		m_Animator.SetBool ("IsRightLegUp", m_RightFoot.position.y > m_LeftFoot.position.y);
//		m_Animator.applyRootMotion = m_IsGrounded;


		if (!m_IsGrounded) {
			//fall faster when in air
			m_RigidBody.AddForce ((Physics.gravity * m_InAirGravityMultiplier) - Physics.gravity);
		}
	}

	private void SetGroundStatus(bool state, Collider other) {
		m_IsGrounded = state;

		if (m_IsGrounded) {
			m_AirManeuversRemaining = m_MaxAirManeuvers;
		}
	}

	private void SetGroundStatus() {
//		Ray ray = new Ray (m_Feet.position, Vector3.down);
//		RaycastHit info;
//		if (Physics.SphereCast (ray, 0.1f, out info, 1f)) {
//			m_IsGrounded = true;
//			m_AirManeuversRemaining = m_MaxAirManeuvers;
//		} else {
//			m_IsGrounded = false;
//		}
	}

	private void OnDash(float v, float h) {
		Vector3 forward = Vector3.Scale(m_Camera.forward, new Vector3(1,0,1)).normalized;
		this.dash = v * forward + h * m_Camera.right;
	}

	void OnAnimatorMove() {
//		if (m_IsGrounded) {
			Vector3 velocity = m_Animator.deltaPosition / Time.deltaTime;
			velocity.y = m_RigidBody.velocity.y;
			m_RigidBody.velocity = velocity;
//		}
	}

}


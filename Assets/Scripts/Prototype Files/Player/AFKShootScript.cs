using UnityEngine;
using System.Collections;
namespace Old {
public class AFKShootScript : MonoBehaviour
{

	public float m_Delay = 1.5f;

	private Transform[] m_Targets;

	private Transform m_Target;

	private PlayerController m_PlayerController;

	private float m_Acculumator = 0f;

	private Vector3 m_StartPosition;

	private Animator m_Animator;

	private bool m_RunToSpawn = false;

	private Transform m_Box;

	private Transform m_Player;
	
	private bool m_Painted = false;

	// Use this for initialization
	void Start ()
	{
		//Find all the targets in the scene
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Targetable");
		m_Targets = new Transform[targets.Length];
		for (int i = 0; i < m_Targets.Length; i++) {
			m_Targets [i] = targets [i].transform;
		}

		m_Animator = GetComponent<Animator> ();
		m_PlayerController = GetComponent<PlayerController> ();
		m_StartPosition = transform.position;
		m_Target = m_Targets[Random.Range(0, m_Targets.Length)];
		m_Box = GameObject.Find ("Box").transform;

		m_Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update() {
		if (!m_Painted) {
			m_PlayerController.CharacterColor = new Color (Random.value, Random.value, Random.value);
			m_Painted = true;
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.position = Vector3.Lerp (transform.position, m_StartPosition, Time.deltaTime);

//		if (Random.value < 0.01f) {
//			m_RunToSpawn = true;
//		}
//
//		if (m_RunToSpawn) {
//			Vector3 distance = m_StartPosition - transform.position;
//
//			Debug.Log (distance.magnitude);
//
//			if(distance.magnitude < 4f) {
//				m_RunToSpawn = false;
//			} else {
//				Quaternion look2 = Quaternion.LookRotation(Vector3.Scale(distance, new Vector3(1,0,1)));
//				transform.rotation = Quaternion.Lerp (transform.rotation, look2, Time.deltaTime);
//			}
//		}



		if (Random.value < 0.01f || Vector3.Magnitude (transform.position - m_Target.position) < 6f) {
			m_Target = Random.value < 0.8f ? m_Targets[Random.Range(0, m_Targets.Length)] : m_Box;
		}

		if (Vector3.Magnitude (transform.position - m_Player.position) < 20f) {
			m_Target = m_Player.transform;
		}

		Vector3 forward = Vector3.Scale (m_Target.position - transform.position, m_Target != m_Box ? new Vector3 (1, 1, 1) : new Vector3 (1, 1, 1));
		if(forward != Vector3.zero) {
			Quaternion look = Quaternion.LookRotation(forward);
			transform.rotation = Quaternion.Lerp (transform.rotation, look, Time.deltaTime*10f);
		}

//		transform.LookAt (m_Target, Vector3.up);

		m_Acculumator += Time.deltaTime;
		if (m_Acculumator > m_Delay) {
			m_PlayerController.Fire ();
			m_Acculumator = 0f;
		}

//		if (Vector3.Magnitude (transform.position - m_Target.position) < 8f) {
//			m_Animator.SetFloat ("Speed", 0f);
//		} else {
//			m_Animator.SetFloat ("Speed", 0.4f);
//		}
	}
}

}
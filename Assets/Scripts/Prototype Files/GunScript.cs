using UnityEngine;
using System.Collections;
namespace Old {
public class GunScript : MonoBehaviour {

	public GameObject m_BulletPrefab;
	public Transform m_SpawnPoint;
	public Transform m_ExitPoint;
	public float power = 10f;

	private Animator m_Animator;

	// Use this for initialization
	void Start () {
		m_Animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if(CanShoot() && Input.GetMouseButtonDown(0)) {
//			m_Animator.SetTrigger("Shoot");
//			Vector3 direction = (m_ExitPoint.position - m_SpawnPoint.position).normalized;
//			GameObject bullet = Instantiate(m_BulletPrefab, m_SpawnPoint.position, Quaternion.LookRotation(direction)) as GameObject;
//			bullet.GetComponent<Rigidbody>().AddForce(direction*power, ForceMode.Impulse);
//			bullet.GetComponent<Animator>().SetTrigger("Wobble");
//		}
	
	}

	public void Fire(Color color, GameObject shooter) {
		if(CanShoot()) {
			m_Animator.SetTrigger("Shoot");
			Vector3 direction = (m_ExitPoint.position - m_SpawnPoint.position).normalized;
			GameObject bullet = Instantiate(m_BulletPrefab, m_SpawnPoint.position, Quaternion.LookRotation(direction)) as GameObject;
			bullet.GetComponent<Renderer>().material.color = color;
			bullet.GetComponent<Rigidbody>().AddForce(direction*power, ForceMode.Impulse);
			bullet.GetComponent<Animator>().SetTrigger("Wobble");
			bullet.GetComponent<BulletScript>().m_Ignore = shooter;
		}
	}

	private bool CanShoot() {
		return m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Ready");
	}
}
}
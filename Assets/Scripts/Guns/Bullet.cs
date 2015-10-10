using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject m_Explosion;

	private Renderer m_Renderer;

//	private Color m_Color;
	private Rigidbody m_RigidBody;
	private Collider m_Collider;
	private float m_Radius;

	private PlayerStats m_Shooter;

	void Awake() {
		//set gameobject to bullet so we ignore bullet/bullet collisions
		gameObject.tag = "Bullet";
		gameObject.layer = LayerMask.NameToLayer ("Bullet");

		m_Renderer = GetComponent<Renderer> ();
	}

	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody> ();
		m_Collider = GetComponent<Collider> ();

		m_Radius = Mathf.Max (m_Collider.bounds.extents.x, m_Collider.bounds.extents.y);
	}

	public void SetStats(PlayerStats playerStats) {
		m_Shooter = playerStats;

		Material m = m_Renderer.material;
		m.color = m_Shooter.PlayerColor;
		m_Renderer.material = m;
	}
	
	// Update is called once per frame
	void Update () {
		//point bullet in direction of velocity
		if (!m_RigidBody.velocity.Equals (Vector3.zero)) {
			transform.forward = m_RigidBody.velocity.normalized;
			Debug.DrawRay(transform.position, transform.forward, Color.red, 1);
		}
	}

	void OnCollisionEnter(Collision collision) {
		PaintableSurface surface = collision.gameObject.GetComponent<PaintableSurface>();

		if (surface == null) {
			return; // Ignore surfaces that aren't paintable
		}

		GameObject obj = Instantiate(m_Explosion, transform.position, Quaternion.identity) as GameObject;
		ParticleSystem[] ps = obj.GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem p in ps) {
			p.startColor = m_Shooter.PlayerColor;
		}
		obj.transform.parent = TempContainer.Instance.transform;

		if (collision.gameObject.tag.Equals("Player")) {
			if(collision.gameObject.GetComponent<PlayerStats>().PlayerColor != m_Shooter.PlayerColor) {
				surface.Paint(m_Shooter, null);
				return;
			}
		}

		//Splat each contact point raycast will only hit the collider of collision
		foreach (ContactPoint p in collision.contacts) {
			//ray from bullet center to contact point
			Ray ray = new Ray(transform.position, p.point - transform.position);

			RaycastHit info;
			if(collision.collider.Raycast(ray, out info, m_Radius*2)) {
				surface.Paint(m_Shooter, info);
			}
		}

//		//extra splats on all objects
//		Vector3[] dirs = {
//
//			(transform.forward + transform.right*0.5f).normalized,
//			(transform.forward - transform.right*0.5f).normalized, //left
//			(transform.forward + transform.up*0.5f).normalized,
//			(transform.forward - transform.up*0.5f).normalized, //down
//
//			(transform.forward + transform.right*0.5f + transform.up*0.5f).normalized,
//			(transform.forward + transform.right*0.5f - transform.up*0.5f).normalized,
//			(transform.forward - transform.right*0.5f + transform.up*0.5f).normalized,
//			(transform.forward - transform.right*0.5f - transform.up*0.5f).normalized,
//
//			(transform.forward + transform.right*1f).normalized,
//			(transform.forward - transform.right*1f).normalized, //left
//			(transform.forward + transform.up*1f).normalized,
//			(transform.forward - transform.up*1f).normalized, //down
//			
//			(transform.forward + transform.right*1f + transform.up*1f).normalized,
//			(transform.forward + transform.right*1f - transform.up*1f).normalized,
//			(transform.forward - transform.right*1f + transform.up*1f).normalized,
//			(transform.forward - transform.right*1f - transform.up*1f).normalized,
//		};
//
//		foreach (Vector3 dir in dirs) {
//			//ray from bullet center to contact point
//			Ray ray = new Ray(transform.position - transform.forward * m_Radius*5, dir);
//
//			Debug.DrawRay(ray.origin, ray.direction, Color.green, 2);
//
//			RaycastHit info;
//			if(Physics.Raycast(ray, out info, 1)) {
//
//				Debug.DrawRay(info.point, info.normal, Color.blue, 2);
//
//				surface = info.collider.gameObject.GetComponent<PaintableSurface>();
//				if(surface != null) {
//					surface.Paint(m_Shooter, info);
////					Debug.Log ("Secondary Hit - with paint");
//				} else {
////					Debug.Log ("Secondary Hit");
//				}
//			} else {
////				Debug.Log ("No Secondary Hit");
//			}
//		}

		Destroy (gameObject);
	}
}

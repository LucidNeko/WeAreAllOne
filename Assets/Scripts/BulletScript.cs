using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public GameObject m_Ignore;

	private Rigidbody m_RigidBody;

	private float m_Lifetime = 7f;

	public void Start() {
		m_RigidBody = GetComponent<Rigidbody> ();
	}

	public void Awake() {
//		StartCoroutine(Expand (3, 1, 0f));
		Destroy (gameObject, m_Lifetime);
	}	

	public void OnCollisionEnter(Collision collision) {
		if (collision.gameObject == m_Ignore) {
			return;
		}

		PaintableSurface ps = collision.gameObject.GetComponent<PaintableSurface> ();
		if (ps != null) {
			if(ps.Paint(GetComponent<Renderer>().material.color, collision)) {
				Destroy (gameObject);
			}
		}
	}

	public IEnumerator Expand(float size, float duration, float delay) {
//		yield return new WaitForSeconds (delay);

		Vector3 start = transform.localScale;
		Vector3 end = new Vector3 (size, size, size);

		float t = 0;
		while (t < 1) {
			transform.localScale = Vector3.Lerp(start, end, t);
			t += Time.deltaTime * duration;
			yield return null;
		}
	}

}

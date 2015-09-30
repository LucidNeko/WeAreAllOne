using UnityEngine;
using System.Collections;

public class DrawRay : MonoBehaviour {

	public Transform m_Barrel;

	private LineRenderer m_LineRenderer;

	private Camera m_Camera;

	private IControl m_Control;

	private bool m_Active = false;

	private PlayerStats m_PlayerStats;

	void Start() {
		m_LineRenderer = GetComponent<LineRenderer> ();
		m_Camera = GetComponentInParent<Camera> ();
		m_Control = GetComponentInParent<IControl> ();
		m_PlayerStats = GetComponentInParent<PlayerStats> ();
	}

	void Update() {
		if (!m_Active && m_Control.GetShoot()) {
			StartCoroutine(Fire ());
		}
	}

	//TODO shooting ray from screen center hits the (invisible) players head when running forward..
	IEnumerator Fire() {
		m_Active = true;

		m_LineRenderer.SetPosition (0, m_Barrel.position);

		Vector2 center = m_Camera.pixelRect.center;
		Ray ray = m_Camera.ScreenPointToRay (new Vector3 (center.x, center.y));
		RaycastHit info;
		if (Physics.Raycast (ray, out info)) {
			Debug.Log ("hit: " + info.collider.gameObject.name);
			PaintableSurface ps = info.collider.gameObject.GetComponent<PaintableSurface>();
			if(ps != null) {
				ps.Paint(m_PlayerStats.PlayerColor, info);
			}
			m_LineRenderer.SetPosition (1, info.point);
		} else {
			m_LineRenderer.SetPosition (1, m_Barrel.position + m_Barrel.forward);
		}

		yield return new WaitForSeconds (0.1f);
		m_LineRenderer.SetPosition (1, m_Barrel.position);
		m_Active = false;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (m_Active) {
			m_LineRenderer.SetPosition (0, m_Barrel.position);

//			Vector2 center = m_Camera.pixelRect.center;
//			Ray ray = m_Camera.ScreenPointToRay (new Vector3 (center.x, center.y));
//			RaycastHit info;
//			if (Physics.Raycast (ray, out info)) {
//				m_LineRenderer.SetPosition (1, info.point);
//			} else {
//				m_LineRenderer.SetPosition (1, m_Barrel.position + m_Barrel.forward);
//			}
		}
	}
}

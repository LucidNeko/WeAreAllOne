using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour, IEquippable
{
	public Vector3 m_Offset;

	private bool m_TriggerDown = false;

	public bool IsTriggerDown() {
		return m_TriggerDown;
	}

	public void TriggerState(bool state) {
		if (!m_TriggerDown && state) {
			TriggerPressed ();
		} else if (m_TriggerDown && !state) {
			TriggerReleased ();
		} else {
			//holding
		}
		
		m_TriggerDown = state;
	}
	
	protected virtual void TriggerPressed() { }

	protected virtual void TriggerReleased() { }

	protected Vector3 GetBulletTrajectory(Camera camera, Transform barrelEnd) {
		Vector2 center = camera.pixelRect.center;
		Ray ray = camera.ScreenPointToRay (new Vector3 (center.x, center.y));
		
		RaycastHit info;
		if (Physics.Raycast (ray, out info)) {
			//			Debug.Log ("hit: " + info.collider.gameObject.name);
			return (info.point - barrelEnd.position).normalized;
		} else {
			return barrelEnd.forward;
		}
	}

	//On equip, after gun has been put in player hierarchy
	public abstract void OnEquip();

	//On drop after gun has been removed from player hierarchy
	public abstract void OnDrop();

	public abstract string GetName ();

}


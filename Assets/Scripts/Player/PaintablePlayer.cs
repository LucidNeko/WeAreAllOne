using UnityEngine;
using System.Collections;

public class PaintablePlayer : MonoBehaviour, Old.PaintableSurface {

	private PlayerStats m_PlayerStats;

	void Start() {
		m_PlayerStats = GetComponent<PlayerStats> ();
	}

	public bool Paint (Color color, Collision info) {
		m_PlayerStats.PlayerColor = color;
		return true;
	}

	public bool Paint (Color color, RaycastHit info) {
		m_PlayerStats.PlayerColor = color;
		return true;
	}

}

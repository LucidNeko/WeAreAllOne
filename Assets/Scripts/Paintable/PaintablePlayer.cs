using UnityEngine;
using System.Collections;

public class PaintablePlayer : MonoBehaviour, PaintableSurface {

	private PlayerStats m_PlayerStats;

	void Start() {
		m_PlayerStats = GetComponent<PlayerStats> ();
	}

	public bool Paint (PlayerStats shooter, Collision info) {
//		m_PlayerStats.PlayerColor = shooter.PlayerColor;
		shooter.HomeSpawn.Spawn (gameObject);
		return true;
	}

	public bool Paint (PlayerStats shooter, RaycastHit info) {
//		m_PlayerStats.PlayerColor = shooter.PlayerColor;
		shooter.HomeSpawn.Spawn (gameObject);
		return true;
	}

}

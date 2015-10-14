using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
	public GameObject m_Effect;

	public GameObject m_DeathEffect;

	public Color m_Color;

	public void Spawn(GameObject player) {
		//drop weapon
		player.GetComponent<GunScript> ().Drop ();

		//Explode effect
		Color explosionColor = player.GetComponent<PlayerStats> ().PlayerColor;
		GameObject explosionffect = Instantiate(m_DeathEffect, player.transform.position, player.transform.rotation) as GameObject;
		foreach (ParticleSystem ps in explosionffect.GetComponentsInChildren<ParticleSystem>()) {
			ps.startColor = explosionColor;
		}
		explosionffect.transform.parent = TempContainer.Instance.transform;

		//create spawn effect
		GameObject effect = Instantiate(m_Effect, transform.position, transform.rotation) as GameObject;
		foreach (ParticleSystem ps in effect.GetComponentsInChildren<ParticleSystem>()) {
			ps.startColor = m_Color;
		}
		effect.transform.parent = TempContainer.Instance.transform;

		//move player
		player.transform.position = transform.position;
		player.transform.rotation = transform.rotation;

		//set color
		player.GetComponent<PlayerStats> ().PlayerColor = m_Color;

		//set home
		player.GetComponent<PlayerStats> ().HomeSpawn = this;
	}
}


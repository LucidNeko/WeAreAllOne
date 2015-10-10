using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
	public GameObject m_Effect;

	public Color m_Color;

	public void Spawn(GameObject player) {
		//drop weapon
		player.GetComponent<GunScript> ().Drop ();

		//create spawn effect
		GameObject effect = Instantiate(m_Effect, transform.position, transform.rotation) as GameObject;
		foreach (ParticleSystem ps in effect.GetComponentsInChildren<ParticleSystem>()) {
			ps.startColor = m_Color;
		}

		//move player
		player.transform.position = transform.position;
		player.transform.rotation = transform.rotation;

		//set color
		player.GetComponent<PlayerStats> ().PlayerColor = m_Color;

		//set home
		player.GetComponent<PlayerStats> ().HomeSpawn = this;
	}
}


using UnityEngine;
using System.Collections;

public class Minigun : Gun {

	public GameObject m_Bullet;
	public Transform m_BarrelEnd;

	public float m_WarmupDelay = 0.7f;
	public float m_BulletSpeed = 50f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Equip(GameObject player) {

	}
	
	public override void Drop() {

	}
}

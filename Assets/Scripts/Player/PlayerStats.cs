using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public Renderer m_PlayerRenderer;
	public Renderer m_GunRenderer;

	private Material m_Material;
	private Material m_NullMaterial;

	public Color PlayerColor {
		get { return m_Material.color; }
		set { m_Material.color = value; }
	}



	void Awake() {
		m_Material = new Material(Shader.Find ("Standard"));
		m_NullMaterial = new Material(Shader.Find ("Standard"));
	}

	void Start() {
		Material[] mats;
		mats = m_PlayerRenderer.materials;
		mats[2] = m_Material;
		Debug.Log("Overriding material " + mats[2].name + " on player");
		mats[4] = m_Material;
		mats[3] = m_Material;
		Debug.Log("Overriding material " + mats[4].name + " on player");
		m_PlayerRenderer.materials = mats;
	}

	public void SetGunRenderer(Renderer gunRenderer) {
		Material[] mats;

		if (m_GunRenderer != null) {
			mats = m_GunRenderer.materials;
			foreach(Material m in mats) {

			}
			mats [1] = m_NullMaterial;
			m_GunRenderer.materials = mats;
		}

		m_GunRenderer = gunRenderer;

		mats = m_GunRenderer.materials;
		mats [1] = m_Material;
		Debug.Log("Overriding material " + mats[1].name + " on gun");
		Debug.Log (mats [1].color.g);
		m_GunRenderer.materials = mats;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.C)) {
			PlayerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		}
	}


}

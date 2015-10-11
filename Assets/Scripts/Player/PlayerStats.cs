using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	private static Color HIJACK_COLOR = new Color(0, 0, 0, 1);

	public Renderer m_PlayerRenderer;
	public Renderer m_HeadRenderer;
	public Renderer m_GunRenderer;

	private Material m_Material;
	private Material m_NullMaterial;

	public Color PlayerColor {
		get { return m_Material.color; }
		set { m_Material.color = value; }
	}

	private PlayerSpawner m_Spawn;
	public PlayerSpawner HomeSpawn {
		get { return m_Spawn; }
		set { m_Spawn = value; }
	}



	void Awake() {
		m_Material = new Material(Shader.Find ("Standard"));
		m_NullMaterial = new Material(Shader.Find ("Standard"));
		m_NullMaterial.color = HIJACK_COLOR;
	}

	void Start() {
		HijackMaterial (m_PlayerRenderer);
		HijackMaterial (m_HeadRenderer);
	}

	public void HijackMaterial(Renderer renderer) {
		Material[] materials = renderer.materials;
		for(int i = 0; i < materials.Length; i++) {
			if(materials[i].color.Equals(m_NullMaterial.color)) {
				//replace
				materials[i] = m_Material;
			}
		}
		renderer.materials = materials;
	}
	
	public void FreeMaterial(Renderer renderer) {
		Material[] materials = renderer.materials;
		for (int i = 0; i < materials.Length; i++) {
			if(materials[i].color == m_Material.color) {
				materials[i] = m_NullMaterial;
			}
		}
		renderer.materials = materials;
	}
}

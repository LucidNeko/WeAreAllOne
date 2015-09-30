using UnityEngine;
using System.Collections;

public class PaintableWall : MonoBehaviour, PaintableSurface
{
	public int m_TextureResolution = 512;
	public Texture2D m_Splat;

	public bool m_Transparant;

	private Texture2D m_Texture;
	
	private Vector3[] m_RandomVectors = new Vector3[100];

	private Color[] m_SplatPixels;

	// Use this for initialization
	void Start ()
	{
		//Create white pixel array
		Color[] white = new Color[m_TextureResolution * m_TextureResolution];
		for(int i = 0; i < m_TextureResolution * m_TextureResolution; i++) {
//			white[i] = Color.white;

			if(m_Transparant) {
				white[i] = new Color(1f,1f,1f,0f);
			} else {
				white[i] = new Color(1f,1f,1f,1f);
			}
		}
		
		//create and assign new white texture
		m_Texture = new Texture2D (m_TextureResolution, m_TextureResolution, TextureFormat.RGBA32, false);
		m_Texture.SetPixels (white);
		m_Texture.Apply ();
		GetComponent<Renderer> ().material.mainTexture = m_Texture;

		//Extract the pixels from the splat effect
		m_SplatPixels = m_Splat.GetPixels ();
	}
	
	public bool Paint(Color color, Collision info) {	
		color.a = 1f;
		foreach (ContactPoint p in info.contacts) {
			RaycastHit hit;
			Ray ray = new Ray (p.point + p.normal, -p.normal);
			if (p.otherCollider.Raycast (ray, out hit, 2f)) {
				Vector2 uv = hit.textureCoord;
				int x = (int)(uv.x * m_Texture.width);
				int y = (int)(uv.y * m_Texture.height);

				for(int row = 0; row < m_Splat.height; row++) {
					for(int col = 0; col < m_Splat.width; col++) {
						Color c0 = m_SplatPixels[row * m_Splat.width + col];
						Color c1 = color;
						if(c0.a != 1f) {
							m_Texture.SetPixel ((int)(x-m_Splat.width/2 + col), (int)(y-m_Splat.height/2 + row), c1);
						}
					}
				}
				m_Texture.Apply ();
			}
		}

		return true;
	}

	public bool Paint(Color color, RaycastHit info) {
		color.a = 1f;
		Vector2 uv = info.textureCoord;
		int x = (int)(uv.x * m_Texture.width);
		int y = (int)(uv.y * m_Texture.height);
		
		for(int row = 0; row < m_Splat.height; row++) {
			for(int col = 0; col < m_Splat.width; col++) {
				Color c0 = m_SplatPixels[row * m_Splat.width + col];
				Color c1 = color;
				if(c0.a != 1f) {
					m_Texture.SetPixel ((int)(x-m_Splat.width/2 + col), (int)(y-m_Splat.height/2 + row), c1);
				}
			}
		}
		m_Texture.Apply ();
		
		return true;
	}

}
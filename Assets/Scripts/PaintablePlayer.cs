using UnityEngine;
using System.Collections;

public class PaintablePlayer : MonoBehaviour, PaintableSurface
{

	public bool m_Debug;

	private PlayerController m_PC;

	// Use this for initialization
	void Start ()
	{
		m_PC = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_Debug) {
			if (Input.GetKeyDown (KeyCode.C)) {
				m_PC.CharacterColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
			}
		}
	}

	public bool Paint(Color color, Collision info) {

		m_PC.CharacterColor = color;

		return true;
	}
}


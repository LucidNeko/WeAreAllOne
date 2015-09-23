using UnityEngine;
using System.Collections;

public class Paintable : MonoBehaviour, Old.PaintableSurface
{

	public bool Paint (Color color, Collision info) {
		GetComponentInChildren<Renderer> ().material.color = color;
		return true;
	}

	public bool Paint (Color color, RaycastHit info) {
		GetComponentInChildren<Renderer> ().material.color = color;
		return true;
	}
}


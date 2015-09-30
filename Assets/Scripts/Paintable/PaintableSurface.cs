using UnityEngine;
using System.Collections;

public interface PaintableSurface {

	bool Paint (Color color, Collision info);
	bool Paint (Color color, RaycastHit info);
}
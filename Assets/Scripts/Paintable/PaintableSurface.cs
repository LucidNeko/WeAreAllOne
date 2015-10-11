using UnityEngine;
using System.Collections;

public interface PaintableSurface {

	bool Paint (PlayerStats shooter, Collision info);
	bool Paint (PlayerStats shooter, RaycastHit info);
}
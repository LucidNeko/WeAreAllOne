using UnityEngine;
using System.Collections;
namespace Old {
public interface PaintableSurface {

	bool Paint (Color color, Collision info);
	bool Paint (Color color, RaycastHit info);
}
}
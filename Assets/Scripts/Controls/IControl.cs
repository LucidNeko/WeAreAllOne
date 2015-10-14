using UnityEngine;
using System.Collections;

public delegate void Dash(float vertical, float horizontal);

public interface IControl
{
	float GetHorizontalAxis ();
	float GetVerticalAxis ();
	float GetHorizontalAxis2 ();
	float GetVerticalAxis2 ();

	float GetShootTrigger ();

	bool GetJump();
	bool GetJumpDown();

	bool GetShoot();
	bool GetShootDown();

	event Dash OnDash;
}


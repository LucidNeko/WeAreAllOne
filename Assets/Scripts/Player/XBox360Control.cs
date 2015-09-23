using UnityEngine;
using System.Collections;

public class XBox360Control : MonoBehaviour, IControl
{

	public event Dash OnDash;

	public float GetHorizontalAxis ()
	{
		return Input.GetAxis("Horizontal");
	}

	public float GetVerticalAxis ()
	{
		return Input.GetAxis("Vertical");
	}

	public float GetHorizontalAxis2 ()
	{
		return Input.GetAxis("Horizontal2");
	}

	public float GetVerticalAxis2 ()
	{
		return Input.GetAxis("Vertical2");
	}

	public bool GetJump ()
	{
		return Input.GetButton ("Jump");
	}

	public bool GetJumpDown ()
	{
		return Input.GetButtonDown ("Jump");
	}

	public bool GetShoot ()
	{
		return Input.GetButton ("Fire1");
	}

	public bool GetShootDown ()
	{
		return Input.GetButtonDown ("Fire1");
	}
}


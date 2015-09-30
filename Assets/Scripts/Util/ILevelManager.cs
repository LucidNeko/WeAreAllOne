using UnityEngine;
using System.Collections;

public interface ILevelManager
{

	/// <summary>
	/// Instantiate and return a new player with a controller of type T
	/// </summary>
	GameObject CreatePlayer<T>() where T : Component, IControl;

}


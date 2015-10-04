using UnityEngine;
using System.Collections;

public interface IEquippable
{
	void Equip(GameObject player);
	void Drop();
}


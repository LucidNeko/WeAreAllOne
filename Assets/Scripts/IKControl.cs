using UnityEngine;
using System;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Animator))] 

public class IKControl : MonoBehaviour {
	
	protected Animator animator;

	public Transform m_GunSlot;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	//a callback for calculating IK
	void OnAnimatorIK()
	{

		Transform leftHand = m_GunSlot.GetComponentsInChildren<Transform> ().FirstOrDefault (t => t.name == "Left Hand");
		Transform rightHand = m_GunSlot.GetComponentsInChildren<Transform> ().FirstOrDefault (t => t.name == "Right Hand");

		if (leftHand != null) { 
			animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
			animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
			animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHand.position);
			animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHand.rotation);
		} else {
			animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
			animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0); 
		}

		if (rightHand != null) {
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
			animator.SetIKPosition(AvatarIKGoal.RightHand,rightHand.position);
			animator.SetIKRotation(AvatarIKGoal.RightHand,rightHand.rotation);
		} else {
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
		}

	}    
}
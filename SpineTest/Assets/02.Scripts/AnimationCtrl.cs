using UnityEngine;
//  using System.Collections;

public class AnimationCtrl : MonoBehaviour 
{
	private Animator ani = null;
	
	public void OnAniRun()
	{
		if (ani == null)
		{
			ani = gameObject.GetComponentInChildren<Animator>();
		}
		ani.SetTrigger ("setJump");
	}
}

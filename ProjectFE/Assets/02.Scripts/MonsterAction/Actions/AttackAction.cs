using UnityEngine;
using System.Collections;
using FreeEvening.Action;
using FreeEvening.Utility;

public class AttackAction : MonsterActionBase
{
#region - protected Methods
	protected override void ProcAction()
	{
		StartCoroutine(MonsterAction());
	}
#endregion

#region - private Methods
	IEnumerator MonsterAction()
	{
		AnimatorStateInfo aniStateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
		if (!aniStateInfo.IsName("shoot"))
		{
			mAnimator.SetTrigger("setShoot");
		}
		while (true)
		{
			yield return Yielders.EndOfFrame;
			aniStateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
			if (aniStateInfo.normalizedTime >= 0.99f)
			{
				StopAction();
			}
		}	
	}
#endregion
}

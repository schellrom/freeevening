using UnityEngine;
using System.Collections;
using FreeEvening.Action;
using FreeEvening.Utility;

public class IdleAction : MonsterActionBase
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
		if (!aniStateInfo.IsName("idle"))
		{
			mAnimator.SetTrigger("setIdle");
		}
		yield return Yielders.GetWaitForSeconds(2.0f);
		StopAction();
	}
#endregion

}

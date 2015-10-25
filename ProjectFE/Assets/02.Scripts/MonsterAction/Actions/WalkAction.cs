using UnityEngine;
using System.Collections;
using FreeEvening.Action;
using FreeEvening.Utility;

public class WalkAction : MonsterActionBase
{
	private float mMarkedTime;
	
#region - Inspector
	public float speed = 1.0f;
#endregion

#region - MonoBehaviour Methods
	void OnTriggerEnter2D(Collider2D coll)
    {
		StopAction();
    }

	void OnTriggerStay2D(Collider2D coll)
    {
		StopAction();
    }
#endregion

#region - protected Methods
	protected override void InitAction()
	{
		CircleCollider2D circleCol2D = (CircleCollider2D)gameObject.GetComponent(typeof(CircleCollider2D));
		circleCol2D.enabled = true;
	}
	
	protected override void EndAction()
	{
		CircleCollider2D circleCol2D = (CircleCollider2D)gameObject.GetComponent(typeof(CircleCollider2D));
		circleCol2D.enabled = false;
	}
	
	protected override void ProcAction()
	{
		StartCoroutine(MonsterAction());
	}
#endregion

#region - private Methods
	IEnumerator MonsterAction()
	{
		AnimatorStateInfo aniStateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
		if (!aniStateInfo.IsName("walk"))
		{
			mAnimator.SetTrigger("setWalk");
		}
		mMarkedTime = Time.time;
		while (true)
		{
			yield return Yielders.EndOfFrame;
			if (IsStoped) StopCoroutine(MonsterAction());
			mRootTrans.position = mRootTrans.position + Vector3.right * speed * (Time.time - mMarkedTime);
			mMarkedTime = Time.time;
		}
	}
#endregion
}

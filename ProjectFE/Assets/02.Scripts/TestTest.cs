using UnityEngine;
using System.Collections;

public class TestTest : MonoBehaviour 
{
	public GameObject mObj = null;
	
	public void OnClickEvent()
	{
		Animator _animatior = (Animator)mObj.GetComponent(typeof(Animator));
		Debug.Log("mAnimatior.GetLayerName(0) : " + _animatior.GetLayerName(0));
		AnimatorStateInfo _aniStateInfo = _animatior.GetCurrentAnimatorStateInfo(0);
		Debug.Log("" + _aniStateInfo.GetHashCode());
		Debug.Log("" + _aniStateInfo.fullPathHash);
		Debug.Log("" + _aniStateInfo.tagHash);
		Debug.Log("" + _aniStateInfo.ToString());
		Debug.Log("" + _aniStateInfo.IsName("Move"));
		_animatior.SetInteger("KnockBack", 2);
	}
}

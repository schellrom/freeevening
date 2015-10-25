using UnityEngine;
using System.Collections;
using Spine;

public class SpineTest : MonoBehaviour
{
    SkeletonUtility sktUtility = null;
    SkeletonAnimation sktAnimation = null;
    SkeletonAnimator sktAnimator = null;
    Animator animator = null;

    // Use this for initialization
    void Start()
    {
        sktAnimator = GetComponent<SkeletonAnimator>();
        if (sktAnimator == null)
        {
            Debug.LogError("sktAnimator == null");
        }
        else
        {
            sktAnimator.GetSkeleton().flipX = true;
            sktAnimator.GetSkeleton().SetColor(new Color(1.0f, 0.0f, 0.0f));
        }
        sktAnimation = GetComponent<SkeletonAnimation>();
        if (sktAnimation == null)
        {
            Debug.LogError("sktAnimation == null");
        }
        else
        {
            Debug.Log("animation : " + sktAnimation.state.GetCurrent(0));
        }
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("animator == null");
        }
        else
        {
            AnimatorClipInfo[] items = animator.GetCurrentAnimatorClipInfo(0);
            Debug.Log("items length : " + items.Length);
            foreach (AnimatorClipInfo item in items)
            {
                Debug.Log("" + item.clip.name);
            }
            Debug.Log("ok");
            //  Debug.Log ("animator : " + animator.GetComponent<UnityEngine.Animation>().name);
        }
        sktUtility = GetComponent<SkeletonUtility>();
        if (sktUtility == null)
        {
            Debug.LogError ("sktUtility == null");
        }
        else
        {
            Debug.Log ("123123" + sktUtility.skeletonAnimation.Skeleton.Slots);
            //  foreach (Slot item in sktUtility.skeletonAnimation.Skeleton.Slots)
            //  {
            //      Debug.Log ("item : " + item.ToString());
            //      if (item.ToString().Equals("front_bracer"))
            //      {
            //          item.SetColor(new Color(0.0f, 1.0f, 0.0f));
            //      }
            //  }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  if (sktAnimator != null)
        //  {
        //      Debug.Log("time : " + sktAnimator.GetSkeleton().time);
        //  }
    }
    
    void Test()
    {
        
    }
}

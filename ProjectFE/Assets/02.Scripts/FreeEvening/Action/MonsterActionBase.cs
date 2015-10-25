using UnityEngine;
using System.Collections;
using FreeEvening.Utility;

namespace FreeEvening.Action
{
    public delegate void ActionDelegate();
    public enum MonsterActionState
    {
        None,
        Idle,
        Walk,
        Attack
    }
    
    /// <summary>몬스터 Action의 base class</summary>
    public abstract class MonsterActionBase : MonoBehaviour
    {
        protected GameObject mModelGameObj = null;
        protected Animator mAnimator = null;
        protected Transform mRootTrans = null;
        private bool mIsStoped = true;
        private ActionDelegate mStopActionDelegate = null;
        
#region - Properties
        protected bool IsStoped
        {
            get { return mIsStoped; }
            set { mIsStoped = value; }
        }
#endregion

#region - MonoBehaviour Methods
        void Awake()
        {
            if (mRootTrans == null)
            {
                mRootTrans = transform.parent;
            }
            if (mModelGameObj == null)
            {
                mModelGameObj = (GameObject)mRootTrans.Find("model").gameObject;
                if (mModelGameObj != null)
                {
                    mAnimator = (Animator)mModelGameObj.GetComponent<Animator>();
                    if (mAnimator == null)
                    {
                        Debug.LogError("can't find Animator in " + mModelGameObj.name);
                    }
                }
                else
                {
                    Debug.LogError("can't find model GameObject in " + transform);
                }
            }
        }
#endregion

#region - public Methods
        /// <summary>start action</summary>
        public void StartAction()
        {
            IsStoped = false;
            mStopActionDelegate = null;
            InitAction();
            ProcAction();
        }
        
        /// <summary>start action</summary>
        /// <param name="_func">action 종료 시 실행할 delegate</param>
        public void StartAction(ActionDelegate _func)
        {
            IsStoped = false;
            mStopActionDelegate = _func;
            InitAction();
            ProcAction();            
        }
        
        /// <summary>stop action</summary>
        public void StopAction()
        {
            if (IsStoped) return;
            IsStoped = true;
            StopAllCoroutines();
            EndAction();
            if (mStopActionDelegate != null)
            {
                mStopActionDelegate();
                mStopActionDelegate = null;
            }
        }
#endregion

#region - protected Methods
        /// <summary>action 초기화(virtual)</summary>
        protected virtual void InitAction() {}
        /// <summary>진행할 action의 구현 부분 (abstract)</summary>
        protected abstract void ProcAction();
        /// <summary>action 종료 시 (virtual)</summary>
        protected virtual void EndAction() {}
#endregion
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FreeEvening.Action
{
	/// <summary>action manager의 base class</summary>
	public abstract class ActionMgrBase : MonoBehaviour
	{
		private Dictionary<string, MonsterActionBase> mActionDic = new Dictionary<string, MonsterActionBase>();
		
		private MonsterActionState mNexttActoin = MonsterActionState.None;
		private MonsterActionState mCurrentActoin = MonsterActionState.None;

		
#region - Inspector
		/// <summary>MonsterAction Prefab</summary>
		[System.Serializable]
		public class ActionEntry
		{
			public string name;
			public GameObject actionGameObj;
		}
		public ActionEntry[] actionEntrys;
		
		public MonsterActionState initAction = MonsterActionState.Idle;
#endregion

#region - Properties
		/// <summary>현재 aciton state</summary>
		public MonsterActionState CurrentAction
		{
			get {return mCurrentActoin;}
		}
		
		/// <summary>다음 aciton state</summary>
		public MonsterActionState NextAction
		{
			get {return mNexttActoin;}
		}
#endregion

#region - MonoBehaviour Methods
		void Awake()
		{
			InitActions();
			InitActionState(initAction);
		}
		
		void OnEnable()
		{
			StartCoroutine(ActionStateLoopFSM());
		}
		
		void OnDisable()
		{
			StopAllCoroutines();
		}
#endregion

#region - public Methods
		/// <summary>start action</summary>
		/// <param name="_actionName">실행할 action name</param>
		public void StartAction(string _actionName)
		{
			if (mActionDic[_actionName] != null)
			{
				mActionDic[_actionName].StartAction();
			}
			else
			{
				Debug.LogError("can't find action from " + _actionName);
			}
		}
		
		/// <summary>start action</summary>
		/// <param name="_actionName">실행할 action name</param>
		/// <param name="_func">action 종료 후 실행할 delegate</param>
		public void StartAction(string _actionName, ActionDelegate _func)
		{
			if (mActionDic[_actionName] != null)
			{
				mActionDic[_actionName].StartAction(_func);
			}
			else
			{
				Debug.LogError("can't find action from " + _actionName);
			}
		}
#endregion

#region - protected Methods
		/// <summary>다음 action을 설정하는 기본 Method (virtual)</summary>
		protected virtual void SetActionState(MonsterActionState _nextActionState)
		{
			mNexttActoin = _nextActionState;
		}
		
		/// <summary>action 초기화</summary>
		protected void InitActionState(MonsterActionState _initActionState)
		{
			mCurrentActoin = MonsterActionState.None;
			mNexttActoin = _initActionState;
		}

		/// <summary>action이 변경될 때 실행되는 부분 (abstract)\n이 Method로 직접 state를 변경하면 안 된다</summary>
		protected abstract void ActionStateFSM(MonsterActionState _nextActionState);
#endregion

#region - private Methods
		IEnumerator ActionStateLoopFSM()
		{
			while (true)
			{
				yield return null;
				if (mCurrentActoin != mNexttActoin)
				{
					mCurrentActoin = mNexttActoin;
					ActionStateFSM(mNexttActoin);
				}
			}
		}
		
		private void InitActions()
		{
			MonsterActionBase monsterAction = null;
			for (int i = 0 ; i < actionEntrys.Length ; i++)
			{
				monsterAction = (MonsterActionBase)actionEntrys[i].actionGameObj.GetComponent(typeof(MonsterActionBase));
				mActionDic.Add(actionEntrys[i].name, monsterAction);
			}
		}		
#endregion

	}
}


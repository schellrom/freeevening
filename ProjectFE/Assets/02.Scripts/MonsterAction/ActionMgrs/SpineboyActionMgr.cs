using UnityEngine;
using FreeEvening.Action;

public class SpineboyActionMgr : ActionMgrBase
{
#region - public Methods
	public void StopActionDelegate()
	{
		switch (CurrentAction)
		{
			case MonsterActionState.Idle:
				SetActionState(MonsterActionState.Walk);
				break;
				
			case MonsterActionState.Walk:
				//  int rnd = Random.Range(0, 2);
				//  if (rnd < 1)
				//  {
				//  	SetActionState(MonsterActionState.Idle);
				//  }
				//  else
				{
					SetActionState(MonsterActionState.Attack);
				}
				break;
				
			case MonsterActionState.Attack:
				SetActionState(MonsterActionState.Idle);
				//  SetActionState(MonsterActionState.Walk);
				break;

		}
	}
#endregion

#region - protected Methods
	protected override void ActionStateFSM(MonsterActionState _nextActionState)
	{
		switch (_nextActionState)
		{
			case MonsterActionState.Idle:
			case MonsterActionState.Walk:
			case MonsterActionState.Attack:
				StartAction(_nextActionState.ToString(), StopActionDelegate);
				break;
		}
	}
#endregion
}

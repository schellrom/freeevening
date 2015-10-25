using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterBase : MonoBehaviour
{
	enum MonsterState
	{
		Walk,
		Idle,
		Attack,
		Hit,
		Dead
	};
	
	Dictionary<string, float> buffs = new Dictionary<string, float>();
	public float moveSpd = 1.0f;
	public float attackRange = 1.0f;
	public int maxHp = 10;
	
	public int currentHp;
	private int nextHp;
	
	private MonsterState currentState = MonsterState.Walk;
	private MonsterState beforeState = MonsterState.Walk;
	private MonsterState nextState = MonsterState.Walk;
	private Animator animator = null;
	
    // Use this for initialization
    void Start()
    {
		buffs.Add("ice", 2.0f);
		animator = GetComponent<Animator>();
		if (animator == null)
		{
			Debug.Log ("Can't Find Animator");
		}
		StartCoroutine(UpdateState());
    }
	
	void OnEnable()
	{
		currentHp = maxHp;
		nextHp = maxHp;
	}
	
	void SetState(MonsterState _state)
	{
		if (animator == null) return;
		switch (_state)
		{
			case MonsterState.Walk:
				break;
				
			case MonsterState.Idle:
				break;
				
			case MonsterState.Attack:
				break;
				
			case MonsterState.Hit:
				break;
				
			case MonsterState.Dead:
				break;
		}
	}
	
	float currentTime = 0.0f;
	IEnumerator UpdateState()
	{
		while (true)
		{
			//  Debug.Log ("delta time : " + (Time.time - currentTime));
			currentTime = Time.time;
			switch (currentState)
			{
				case MonsterState.Walk:
					break;
					
				case MonsterState.Idle:
					break;
					
				case MonsterState.Attack:
					break;
					
				case MonsterState.Hit:
					break;
					
				case MonsterState.Dead:
					break;
			}
			yield return new WaitForSeconds(5.0f);
		}
	}
	
	string TestMethod(int _num)
	{
		return _num.ToString();
	}
}

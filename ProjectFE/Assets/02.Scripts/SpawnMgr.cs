using UnityEngine;
using System.Collections;

/// <summary>
/// 일정 시간 마다 캐릭터를 생성
/// </summary>
public class SpawnMgr : MonoBehaviour
{
	public GameObject[] monster;
	public float minSpawnTime = 1.0f;
	public float maxSpawnTime = 3.0f;
	
	private Transform tr = null;
	private Transform stageTran = null;
	
    void Start()
    {
		tr = gameObject.transform;
		stageTran = tr.parent.transform;
		Invoke ("SpawnMonster", minSpawnTime);
    }

    void SpawnMonster()
    {
		StartCoroutine(CreateMonster());
		
		float delayTime = Random.Range(minSpawnTime, maxSpawnTime);
		//  Invoke ("SpawnMonster", delayTime);
    }
	
	IEnumerator CreateMonster()
	{
		GameObject monsterObj = (GameObject)GameObject.Instantiate(monster[0], tr.position, Quaternion.identity);
		monsterObj.transform.parent = stageTran;
		monsterObj.transform.localScale = new Vector3(40.0f, 40.0f, 1.0f);
		
		yield return null;
	}
}

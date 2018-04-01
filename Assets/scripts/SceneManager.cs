using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyLevel : uint
{
	Zombie = 1
}

public class SceneManager : MonoBehaviour {

    public GameObject player;

    public float sceneWidth = 100;
    public float sceneLength = 100;

	GameObject zombie;
	Dictionary<uint, GameObject> allEnemy = new Dictionary<uint, GameObject>();
	Dictionary<uint, GameObject> inactiveEnemy = new Dictionary<uint, GameObject>();

	//Difficulty increasing by time for now;
	uint difficulty = 0;

	// Use this for initialization
	void Start () 
	{
		difficulty = 1;
		zombie = Resources.Load("prefabs/zombie") as GameObject;
        zombie.GetComponent<EnemyZombie>().target = player;
		CreateEnemy((uint)EnemyLevel.Zombie, zombie);
	}
	
	// Update is called once per frame
	void Update () 
	{
		uint currentDifficulty = 0;
		foreach (KeyValuePair<uint, GameObject> enemyInfo in allEnemy)
		{
			if (enemyInfo.Value.activeSelf) {
				currentDifficulty += enemyInfo.Key;
			}
			else 
			{
				inactiveEnemy.Add (enemyInfo.Key, enemyInfo.Value);
			}
		}

		if (currentDifficulty < difficulty) 
		{
			Vector3 position = new Vector3(Random.Range(-sceneWidth / 2, sceneWidth / 2), 0, Random.Range(-sceneLength / 2, sceneLength / 2));

			//Randomize an enemy
			if (inactiveEnemy.ContainsKey ((uint)EnemyLevel.Zombie)) {
				GameObject enemy = inactiveEnemy [(uint)EnemyLevel.Zombie];
				enemy.transform.position = position;
				enemy.SetActive (true);

				inactiveEnemy.Remove ((uint)EnemyLevel.Zombie);
			}
			else 
			{
				CreateEnemy ((uint)EnemyLevel.Zombie, zombie);
			}
		}
	}

	void CreateEnemy(uint enemyLevel, GameObject enemy)
    {
        if(enemy)
        {
            Vector3 position = new Vector3(Random.Range(-sceneWidth / 2, sceneWidth / 2), 0, Random.Range(-sceneLength / 2, sceneLength / 2));
            Quaternion quaternion = new Quaternion(0, 0, 0, 0);
			allEnemy.Add (enemyLevel, Instantiate (enemy, position, quaternion));
        }
    }
}

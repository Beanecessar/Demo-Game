using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class SceneManager : MonoBehaviour {

    public GameObject player;

    public float sceneWidth = 100;
    public float sceneLength = 100;
	public uint maxDifficuty = 10;

	EnemyManager enemyManager;

	//Difficulty increasing by time for now;
	uint difficulty = 0;

	// Use this for initialization
	void Start () 
	{
		difficulty = maxDifficuty;
		enemyManager = ScriptableObject.CreateInstance<EnemyManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		uint currentDifficulty = 0;

		enemyManager.CheckActive ();

		foreach (EnemyInfo enemyInfo in enemyManager.activeEnemy) 
		{
			currentDifficulty += enemyInfo.difficultyLevel;
		}

		if (currentDifficulty < difficulty) 
		{
			Vector3 position = new Vector3(Random.Range(-sceneWidth / 2, sceneWidth / 2), 0, Random.Range(-sceneLength / 2, sceneLength / 2));
			Quaternion quaternion = new Quaternion(0, 0, 0, 0);

			//TODO:Randomize an enemy
			enemyManager.CreateEnemy (EnemyType.Zombie,position,quaternion);
		}
	}
}

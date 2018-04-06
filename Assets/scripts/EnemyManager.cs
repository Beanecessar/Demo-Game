using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public enum EnemyType
	{
		Zombie,
		Slime
	}

	public struct EnemyInfo
	{
		public int identifier;
		public EnemyType type;
		public uint difficultyLevel;
		public GameObject instance;
	}

	public class EnemyManager : ScriptableObject
	{
		public EnemyManager()
		{
			enemyPrefabs = new Dictionary<EnemyType, GameObject> ();
			activeEnemy = new List<EnemyInfo> ();
			inactiveEnemy = new List<EnemyInfo> ();
		}

		public void OnEnable()
		{
			
		}

		public void OnDisable()
		{

		}

		Dictionary<EnemyType, GameObject> enemyPrefabs;

		public List<EnemyInfo> activeEnemy;
		public List<EnemyInfo> inactiveEnemy;

		public void CheckActive()
		{
			for (int i = inactiveEnemy.Count-1; i>=0; --i) 
			{
				if (inactiveEnemy[i].instance.activeSelf) 
				{
					activeEnemy.Add (inactiveEnemy[i]);
					inactiveEnemy.Remove (inactiveEnemy[i]);
				}
			}
			for (int i = activeEnemy.Count-1; i>=0; --i) 
			{
				if (!activeEnemy[i].instance.activeSelf) 
				{
					inactiveEnemy.Add (activeEnemy[i]);
					activeEnemy.Remove (activeEnemy[i]);
				}
			}
		}

		public EnemyInfo CreateEnemy(EnemyType type, Vector3 position, Quaternion quaternion, bool active = true)
		{
			EnemyInfo enemyInfo = FindInactiveEnemy(type);
			if (enemyInfo.identifier > 0) 
			{
				enemyInfo.identifier = GenerateIdentifier ();
				enemyInfo.type = type;
				enemyInfo.difficultyLevel = GetDifficulty (type);
				enemyInfo.instance.transform.position = position;
				enemyInfo.instance.transform.rotation = quaternion;
				enemyInfo.instance.SetActive (true);
			}
			else
			{
				enemyInfo.identifier = GenerateIdentifier ();
				enemyInfo.type = type;
				enemyInfo.difficultyLevel = GetDifficulty (type);

				if (!enemyPrefabs.ContainsKey (type)) 
				{
					GameObject prefab = Resources.Load (GetPrefabPath (type)) as GameObject;
					enemyPrefabs.Add (type, prefab);
				}

				enemyInfo.instance = Instantiate (enemyPrefabs[type], position, quaternion);
				if (active)
					activeEnemy.Add (enemyInfo);
				else
					inactiveEnemy.Add (enemyInfo);
			}

			return enemyInfo;
		}

		EnemyInfo FindInactiveEnemy(EnemyType type)
		{
			EnemyInfo enemyInfo = new EnemyInfo ();
			enemyInfo.identifier = -1;
			foreach (EnemyInfo info in inactiveEnemy) 
			{
				if (info.type == type) 
				{
					enemyInfo = info;
					break;
				}
			}
			return enemyInfo;
		}

		//TODO: Optimise
		int GenerateIdentifier()
		{
			int indentifier = UnityEngine.Random.Range (1, 10000);
			return indentifier;
		}

		//Define enemy difficulty here
		static uint GetDifficulty(EnemyType type)
		{
			switch (type) 
			{
			case EnemyType.Zombie:
				return 1;
			case EnemyType.Slime:
				return 2;
			default:
				return 0;
			}
		}

		//Define enemy prefab path here
		static string GetPrefabPath(EnemyType type)
		{
			switch (type) 
			{
			case EnemyType.Zombie:
				return "prefabs/zombie";
			case EnemyType.Slime:
				return "prefabs/slime";
			default:
				return "";
			}
		}
	}
}


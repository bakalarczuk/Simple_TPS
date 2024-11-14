using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Transform> spawns;
	public GameObject heavyEnemyPrefab;
	public GameObject lightEnemyPrefab;
	public List<GameObject> spawnedEnemies;

	private float timer = 0;

	public int maxSpawnedEnemies = 5;

	private void Start()
	{
		List<int> spawnsIndices = TakeSpawn(maxSpawnedEnemies, spawns);
		for (int i = 0; i < spawnsIndices.Count; i++)
		{
			GameObject enemy = Instantiate(Random.value > 0.5 ? heavyEnemyPrefab : lightEnemyPrefab, spawns[spawnsIndices[i]], false);
			enemy.GetComponent<Enemy>().controller = this;
			spawnedEnemies.Add(enemy);
		}
	}


	private void Update()
	{
		timer += Time.deltaTime;
		//if(timer >= )
	}

	private List<int> TakeSpawn(int Length, List<Transform> collection)
	{
		List<int> list = new List<int>(new int[Length]);

		for (int j = 1; j < Length; j++)
		{
			int Rand = Random.Range(0, collection.Count);

			while (list.Contains(Rand))
			{
				Rand = Random.Range(0, collection.Count);
			}

			list[j] = Rand;
		}

		return list;
	}
}

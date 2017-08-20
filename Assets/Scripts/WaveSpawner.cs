using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameLogic))]
public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING}

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform[] enemy;
		public int count;
		public float rate;
	}

	private int numberOfTimesLooped = 0;

	public Wave[] waves;
	private int nextWave = 0;

	public Transform[] spawnPoints;

	//public float timeBetweenWaves = 0f;
	//private float waveCountdown;

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;

	private void Start()
	{
		//waveCountdown = timeBetweenWaves;

		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No enemy spawnpoints");
		}

	}

	private void Update()
	{
		if(state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		//if(waveCountdown <= 0)
		//{
			//void StartWave()
			//{
			///	StartCoroutine(SpawnWave(waves[nextWave]));
			//}
		//}
		//else
		//{
			//waveCountdown -= Time.deltaTime;
		//}
	}
	
	public void StartWave()
	{
		StartCoroutine(SpawnWave(waves[nextWave]));

		GetComponent<GameLogic>().waveNumber++;
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if(GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}

		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy[numberOfTimesLooped]);
			yield return new WaitForSeconds(1f / _wave.rate);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void WaveCompleted()
	{
		state = SpawnState.COUNTING;
		//waveCountdown = timeBetweenWaves;

		if(nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			numberOfTimesLooped++;
		}
		else
		{
			nextWave++;
		}
	}

	void SpawnEnemy (Transform _enemy)
	{
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Instantiate(_enemy, _sp.position, _sp.rotation);
	}

}

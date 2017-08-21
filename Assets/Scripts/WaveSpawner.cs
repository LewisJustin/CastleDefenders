using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameLogic))]
public class WaveSpawner : MonoBehaviour
{

	private bool hasBeenRewardedThisRound = false;

	[SerializeField] GameObject panel;

	public enum SpawnState {SPAWNING, WAITING}

	[System.Serializable]
	public class Wave
	{
		//public string name;
		public int count;
		public float rate;
		public Transform enemy;
	}

	private int numberOfTimesLooped;

	public Wave[] waves;
	private int nextWave = 0;

	[HideInInspector] public int enemiesInThisWave;

	public Transform[] spawnPoints;

	//public float timeBetweenWaves = 0f;
	//private float waveCountdown;

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.WAITING;

	private void Start()
	{
		//waveCountdown = timeBetweenWaves;

		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No enemy spawnpoints");
		}

	}

	private void Awake()
	{
		numberOfTimesLooped = 0;
	}

	private void Update()
	{
		

		if (enemiesInThisWave == 0)
		{
			state = SpawnState.WAITING;
			panel.SetActive(true);
		}
		else
		{
			panel.SetActive(false);
		}

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
		if (state == SpawnState.WAITING)
		{
			StartCoroutine(SpawnWave(waves[nextWave]));

			GetComponent<GameLogic>().castleHealth = GetComponent<GameLogic>().castleMaxHealth;

			GetComponent<GameLogic>().waveNumber++;
		}
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
		hasBeenRewardedThisRound = false;
		for (int i = 0; i < _wave.count; i++)
		{
			//Debug.Log(numberOfTimesLooped);
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f / Random.Range(_wave.rate-.2f, _wave.rate+.2f));
		}

		yield break;
	}

	void WaveCompleted()
	{
		state = SpawnState.WAITING;
		//waveCountdown = timeBetweenWaves;

		if (!hasBeenRewardedThisRound)
		{
			GetComponent<GameLogic>().currency += 100;
			hasBeenRewardedThisRound = true;
		}


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
		enemiesInThisWave++;
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Transform newObject = Instantiate(_enemy, _sp.position, _sp.rotation);
		newObject.name = "Enemy";
	}

}

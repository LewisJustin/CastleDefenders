using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameLogic))]
public class WaveSpawner : MonoBehaviour
{

	private bool hasBeenRewardedThisRound = false;

	[SerializeField] GameObject storePanel;
	[SerializeField] GameObject saveButton;
	[SerializeField] GameObject loadButton;
	[SerializeField] Transform enemyParent;

	[SerializeField] GameObject audioManager;

	//enums, in this case, is a variable
	//where you can have multiple states
	//In the same way a bool can have 2
	public enum SpawnState {SPAWNING, WAITING}

	[System.Serializable]
	public class Wave
	{
		//public string name;
		public int count;
		public float rate;
		public Transform[] enemy;
	}

	public Wave[] waves;
	private int nextWave;

	[HideInInspector] public int enemiesInThisWave;

	public Transform[] spawnPoints;
	
	private SpawnState state = SpawnState.WAITING;

	private void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No enemy spawnpoints");
		}
		GetComponent<GameLogic>().currency += 100;
		hasBeenRewardedThisRound = true;
	}

	private void Awake()
	{
		nextWave = -1;
	}

	private void Update()
	{
		if (enemiesInThisWave == 0)
		{
			state = SpawnState.WAITING;

			saveButton.SetActive(true);
			loadButton.SetActive(true);

			WaveCompleted();
		}
		else
		{
			saveButton.SetActive(false);
			loadButton.SetActive(false);
		}
	}
	
	//This is called from the Start Wave Button
	public void StartWave()
	{
		if (state == SpawnState.WAITING)
		{
			//Keeps track of which wave we are on,
			//If we have gone through all of the waves
			//Reset nextWave variable
			if(nextWave + 1 > waves.Length - 1)
			{
				nextWave = 0;
			}
			else
			{
				nextWave++;
			}

			storePanel.SetActive(false);
			
			//Starts Spawning the wave
			StartCoroutine(SpawnWave(waves[nextWave]));

			//Increases wave number text
			//of the top bar in the game
			GetComponent<GameLogic>().waveNumber++;
		}
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;
		
		//Makes sure we only get wave bonus once.
		hasBeenRewardedThisRound = false;


		for (int i = 0; i < _wave.count; i++)
		{
			//Spawns a random enemy from our array of enemies,
			SpawnEnemy(_wave.enemy[Random.Range(0, _wave.enemy.Length)], _wave.count);
			//Puts a delay between enemies, and makes delay slightly random
			yield return new WaitForSeconds(1f / Random.Range(_wave.rate-.2f, _wave.rate+.2f));
		}

		yield break;
	}

	void WaveCompleted()
	{
		state = SpawnState.WAITING;
		//waveCountdown = timeBetweenWaves;

		//Resets castle health
		GetComponent<GameLogic>().castleHealth = GetComponent<GameLogic>().castleMaxHealth;

		if (!hasBeenRewardedThisRound)
		{
			GetComponent<GameLogic>().currency += 100;
			audioManager.GetComponent<AudioManager>().Play("EndWave");
			hasBeenRewardedThisRound = true;
		}

	}

	public void SpawnIndividualEnemy(int direction, Object enemy)
	{
		if(direction == 0)
		{
			Transform _sp = spawnPoints[0];
			//Instantiates enemy a the selected spawnpoint from before
			Transform newEnemy = Instantiate((Transform)enemy, _sp.position, _sp.rotation);
			newEnemy.name = "Custom Enemy";
			newEnemy.SetParent(enemyParent);
			newEnemy.GetComponent<EnemyLogic>().SetDirection();
		}
		else if (direction == 1)
		{
			Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
			//Instantiates enemy a the selected spawnpoint from before
			Transform _newEnemy = Instantiate(enemy, _sp.position, _sp.rotation) as Transform;
			_newEnemy.name = "Custom Enemy";
			_newEnemy.SetParent(enemyParent);
			_newEnemy.GetComponent<EnemyLogic>().SetDirection();
		}
		else if(direction == 2)
		{
			//spawn enemy on the right
			Transform _sp = spawnPoints[1];
			//Instantiates enemy a the selected spawnpoint from before
			Transform newEnemy = Instantiate((Transform)enemy, _sp.position, _sp.rotation);
			newEnemy.name = "Custom Enemy";
			newEnemy.SetParent(enemyParent);
			newEnemy.GetComponent<EnemyLogic>().SetDirection();
		}
	}

	void SpawnEnemy (Transform _enemy, int count)
	{
		enemiesInThisWave++;
		//Sets a spawn point from our array of spawn points,
		//Is an array so if we add flying enemies, they will be
		//able to spawn from there, though we will have to change this slightly
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		//Instantiates enemy a the selected spawnpoint from before
		Transform newEnemy = Instantiate(_enemy, _sp.position, _sp.rotation);
		newEnemy.name = "Enemy" + (enemiesInThisWave - 1);
		newEnemy.SetParent(enemyParent);
		
		newEnemy.GetComponent<EnemyLogic>().SetDirection();
	}

}

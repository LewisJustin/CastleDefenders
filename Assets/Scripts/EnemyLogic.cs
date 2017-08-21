using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private GameObject toDestory;
	[SerializeField] private bool ranged;
	[SerializeField] private GameObject GameManager;
	[SerializeField] private float speed;
	[SerializeField] private int arrowDamage;

	public int health;
	private bool hasArrived;
	private int damage;

	//public void TakeDamage(int _damage)
	//{
	//	health -= _damage;
	//}

	

	private void Awake()
	{
		health = 100;
		speed = 2.5f;
		hasArrived = false;
		damage = 10;

		GameManager = GameObject.Find("GameManager");
	}

	void Update()
	{
		if (health <= 0)
		{
			GameManager.GetComponent<WaveSpawner>().enemiesInThisWave--;

			Destroy(toDestory);
		}
			

		#region GettingToLocation
		if (!ranged && !hasArrived)
		{
			if (transform.position.x < 5)
			{
				transform.Translate(Vector3.right * speed * Time.deltaTime);
			}
			else
			{
				StartCoroutine(StartAttackingMelee());
				hasArrived = true;
			}
		}

		if (ranged && !hasArrived)
		{
			if (transform.position.x < 8)
			{
				transform.Translate(Vector3.right * speed * Time.deltaTime);
			}
			else
			{
				StartCoroutine(StartAttackingRanged());
				hasArrived = true;
			}
		}
		#endregion
	}

	IEnumerator StartAttackingMelee()
	{
		while (health > 0)
		{
			GameManager.GetComponent<GameLogic>().castleTakeDamage(damage);

			yield return new WaitForSeconds(3f);
		}
	}

	IEnumerator StartAttackingRanged()
	{

		while (health > 0)
		{
			GameManager.GetComponent<GameLogic>().castleTakeDamage(damage);

			yield return new WaitForSeconds(3f);
		}
	}
	
}

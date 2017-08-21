using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private bool ranged;
	[SerializeField] private GameObject GameManager;
	[SerializeField] private float speed;
	
	public int health;
	private bool hasArrived;
	public int damage; 

	private void Awake()
	{
		hasArrived = false;
		

		GameManager = GameObject.Find("GameManager");
	}

	void Update()
	{
        if (health <= 0)
            Destroy(gameObject);

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
			if (transform.position.x < 2)
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

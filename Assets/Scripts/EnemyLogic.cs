using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private GameObject toDestory;
	[SerializeField] private bool ranged;

	private float speed;
	private int health= 100;
	private bool hasArrived = false;

	private void Awake()
	{
		health = 100;
	}

	void Update()
	{
		if (health <= 0)
			Destroy(toDestory);

		#region GettingToLocation
		if (!ranged && !hasArrived)
		{
			if (transform.position.x < 5)
			{
				transform.Translate(Vector3.right * speed * Time.deltaTime);
			}
			else
			{
				StartCoroutine(startAttackingMelee());
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
				StartCoroutine(startAttackingRanged());
				hasArrived = true;
			}
		}
		#endregion
	}

	IEnumerator startAttackingMelee()
	{
		while (health > 0)
		{
			Debug.Log("attack melee");

			yield return new WaitForSeconds(3f);
		}
	}

	IEnumerator startAttackingRanged()
	{
		while (health > 0)
		{
			Debug.Log("attack ranged");

			yield return new WaitForSeconds(3f);
		}
	}
	
}
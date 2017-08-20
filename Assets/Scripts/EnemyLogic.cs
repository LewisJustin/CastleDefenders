using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private Transform toDestory;

	public float speed = 1f;
	public int health;
	public bool ranged;

	void Update()
	{
		if (!ranged)
		{
			if (toDestory.position.x < 5)
			{
				transform.Translate(Vector3.right * speed * Time.deltaTime);
			}
			else
			{
				StartCoroutine(startAttackingMelee());
			}
		}

		if (ranged)
		{
			if (toDestory.position.x < 8)
			{
				transform.Translate(Vector3.right * speed * Time.deltaTime);
			}
			else
			{
				StartCoroutine(startAttackingRanged());
			}
		}
	}

	IEnumerator startAttackingMelee()
	{
		Debug.Log("attack melee");

		yield return new WaitForSeconds(3f);
	}

	IEnumerator startAttackingRanged()
	{
		Debug.Log("attack ranged");

		yield return new WaitForSeconds(3f);
	}
	
}

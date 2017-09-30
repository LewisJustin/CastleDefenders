using UnityEngine;
using System.Collections;

public class RayCasting : MonoBehaviour {

	//public float castingRate = 1.5f;
	public LayerMask toHit;
	public LayerMask castleLayer;
	public float distanceBetweenEnemies;
	
	
	void Start ()
	{
		if(!transform.parent.GetComponent<EnemyLogic>().goingRight)
		{
			transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
		}

		StartCoroutine(Shoot());
	}

	IEnumerator Shoot()
	{
		RaycastHit2D enemiesHit;
		RaycastHit2D castleHit;
		for(int i = 0; i < 10;)
		{
			yield return new WaitForSeconds(.01f);

			if(transform.parent.GetComponent<EnemyLogic>().goingRight)
			{
				enemiesHit = Physics2D.Raycast(transform.position, Vector2.right, distanceBetweenEnemies, toHit);

				if(transform.parent.GetComponent<EnemyLogic>().ranged)
				{
					castleHit = Physics2D.Raycast(transform.position, Vector2.right, transform.parent.GetComponent<EnemyLogic>().stoppingDistanceRanged, castleLayer);
				}
				else
				{
					castleHit = Physics2D.Raycast(transform.position, Vector2.right, transform.parent.GetComponent<EnemyLogic>().stoppingDistanceMelee, castleLayer);
				}
			}
			else
			{
				enemiesHit = Physics2D.Raycast(transform.position, Vector2.left, distanceBetweenEnemies, toHit);

				if(transform.parent.GetComponent<EnemyLogic>().ranged)
				{
					castleHit = Physics2D.Raycast(transform.position, Vector2.left, transform.parent.GetComponent<EnemyLogic>().stoppingDistanceRanged, castleLayer);
				}
				else
				{
					castleHit = Physics2D.Raycast(transform.position, Vector2.left, transform.parent.GetComponent<EnemyLogic>().stoppingDistanceMelee, castleLayer);
				}
			}

			if(castleHit.collider != null)
			{
				transform.parent.GetComponent<EnemyLogic>().hasArrived = true;
			}
			else
			{
				transform.parent.GetComponent<EnemyLogic>().hasArrived = false;
			}

			if(enemiesHit.collider != null)
			{
				transform.parent.GetComponent<EnemyLogic>().canMove = false;
			}
			else
			{
				transform.parent.GetComponent<EnemyLogic>().canMove = true;
			}

		}

	}
}
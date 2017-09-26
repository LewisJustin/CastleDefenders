using UnityEngine;
using System.Collections;

public class RayCasting : MonoBehaviour {

	//public float castingRate = 1.5f;
	public LayerMask toHit;
	public float distanceBetweenEnemies;

	Transform firePoint;

	Vector3 position;
	
	void Start ()
	{
		firePoint = transform;
		if(firePoint == null)
		{
			Debug.LogError("No firepoint");
		}

		StartCoroutine(Shoot());
	}

	IEnumerator Shoot()
	{
		if(transform.parent.GetComponent<EnemyLogic>().goingRight)
		{
			for (int i = 0; i < 10;)
			{
				yield return new WaitForSeconds(.01f);
				
				//Debug.DrawLine(transform.position, Vector2.right, Color.green);

				RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, distanceBetweenEnemies, toHit);

				if (hit.collider != null)
				{
						transform.parent.GetComponent<EnemyLogic>().canMove = false;
				}
				else
				{
					transform.parent.GetComponent<EnemyLogic>().canMove = true;
				}

			}
		}
		else
		{
			for (int i = 0; i < 10;)
			{
				yield return new WaitForSeconds(.01f);
				
				//Debug.DrawLine(transform.position, Vector2.right, Color.green);

				position = transform.position;
				position.x -= 1f;

				RaycastHit2D hit = Physics2D.Raycast(position, Vector2.left, distanceBetweenEnemies, toHit);

				if (hit.collider != null)
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
}

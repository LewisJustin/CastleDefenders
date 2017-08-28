using UnityEngine;
using System.Collections;

public class RayCasting : MonoBehaviour {

	//public float castingRate = 1.5f;
	public LayerMask toHit;
	public float distanceBetweenEnemies = 3f;

	Transform firePoint;
	
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
		for (int i = 0; i < 10;)
		{
			yield return new WaitForSeconds(1.5f);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, distanceBetweenEnemies, toHit);

			if (hit.collider != null)
			{
				//if (hit.transform.position.x - transform.position.x < distanceBetweenEnemies)
				//{
					transform.parent.GetComponent<EnemyLogic>().canMove = false;
<<<<<<< HEAD:Assets/RayCasting.cs
						transform.parent.GetComponent<EnemyLogic>().animator.SetBool("isAtTarget", true);
				//}
=======
					transform.parent.GetComponent<EnemyLogic>().animator.SetBool("isAtTarget", true);
				}
>>>>>>> a6670513978a5f2cd341798db57648e896d94213:Assets/Scripts/RayCasting.cs
			}
			else
			{
				transform.parent.GetComponent<EnemyLogic>().canMove = true;
				if (!transform.parent.GetComponent<EnemyLogic>().hasArrived)
					transform.parent.GetComponent<EnemyLogic>().animator.SetBool("isAtTarget", false);
			}

		}
		
	}
}

using UnityEngine;
using System.Collections;

public class RayCasting : MonoBehaviour {

	//public float castingRate = 1.5f;
	public LayerMask toHit;
	[HideInInspector]public float distanceBetweenEnemies = 2f;

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
			yield return new WaitForSeconds(.01f);
			
			//Debug.DrawLine(transform.position, Vector2.right, Color.green);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, distanceBetweenEnemies, toHit);

			if (hit.collider != null)
			{
					transform.parent.GetComponent<EnemyLogic>().canMove = false;

					transform.parent.GetComponent<EnemyLogic>().animator.SetBool("isAtTarget", true);
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

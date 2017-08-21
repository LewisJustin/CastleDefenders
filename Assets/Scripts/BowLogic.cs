using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowLogic : MonoBehaviour {

	[SerializeField] Rigidbody2D ProjectilePrefab;

	[HideInInspector] public float bowDrawSpeed = 1f;

	private bool canShoot = true;

	void Update()
	{
		#region LookAtMouseKindOf
		Vector3 dir = Input.mousePosition - (transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 39;
		transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
		#endregion

		if (Input.GetMouseButtonDown(0) && canShoot == true)
		{
			StartCoroutine(ShootingCountdown());

			Rigidbody2D projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(  0f, 180f,0f));

			projectile.velocity = transform.TransformDirection(Vector3.left * 10);
		}
	}

	IEnumerator ShootingCountdown()
	{
		canShoot = false;
		yield return new WaitForSeconds(bowDrawSpeed);
        Debug.Log("Shooting Bow");
		canShoot = true;
	}
}


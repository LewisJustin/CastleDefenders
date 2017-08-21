using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowLogic : MonoBehaviour {

	[SerializeField] Rigidbody2D ProjectilePrefab;

	[HideInInspector] public float bowDrawSpeed = 1f;

	private bool canShoot = true;

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && canShoot == true)
		{
			StartCoroutine(ShootingCountdown());

			Rigidbody2D projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));

			projectile.velocity = transform.TransformDirection(Vector3.left * 10);
		}
	}
	
	IEnumerator ShootingCountdown()
	{
		canShoot = false;
		Debug.Log("Shooting Bow");
		yield return new WaitForSeconds(bowDrawSpeed);
		canShoot = true;
	}
}


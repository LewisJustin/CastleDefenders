using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowLogic : MonoBehaviour {

	[SerializeField] Rigidbody2D ProjectilePrefab;

	public float bowDrawSpeed;
	 public int damage = 25;

	private bool canShoot = true;

	private void Awake()
	{
		bowDrawSpeed = 3f;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && canShoot == true)
		{
			StartCoroutine(ShootingCountdown());

			Rigidbody2D projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));

			projectile.velocity = transform.TransformDirection(Vector3.left * 20);
		}
	}
	
	IEnumerator ShootingCountdown()
	{
		canShoot = false;
		//Debug.Log("Shooting Bow");
		yield return new WaitForSeconds(bowDrawSpeed);
		canShoot = true;
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour {

	//private float x = 180f;
	//private bool y = true;
	[SerializeField] private GameObject toDestroy;
    [SerializeField]private float TimeToDestroy;

	public int damage;

	private void Awake()
    {
		damage = 100;
    }

	private void Update()
	{
		Vector2 moveDirection = transform.GetComponent<Rigidbody2D>().velocity;
		if (moveDirection != Vector2.zero)
		{
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		transform.GetComponent<Rigidbody2D>().freezeRotation = true;
		transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		//y = false;
		//Debug.Log("working");
		damage = 0;

		StartCoroutine(DestroyMe());
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(toDestroy);
	}

	IEnumerator DestroyMe()
	{

		yield return new WaitForSeconds(TimeToDestroy);
		Destroy(toDestroy);
	}
}

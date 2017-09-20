using UnityEngine;

public class ArcherArrowLogic : MonoBehaviour {

	[SerializeField] GameObject toDestroy;

	private void Start()
	{
		Destroy(toDestroy, 3f);
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
	}

}

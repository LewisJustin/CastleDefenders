using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private Transform toDestory;

	public float speed = 1f;
	public int health;
	public bool ranged;

	void Update()
	{
		if (!ranged)
			if (toDestory.position.x < 5)
				transform.Translate(Vector3.right * speed * Time.deltaTime);
		if (ranged)
			if (toDestory.position.x < 8)
				transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
}

using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private GameObject toDestory;

		public float speed = 1f;
		public int health;
		public bool ranged;

	void Update()
	{
		transform.Translate(Vector3.right * speed * Time.deltaTime);

	}
}

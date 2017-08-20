using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private GameObject toDestory;

	public float speed = 1f;
	public int health;
	public bool ranged;
	public bool isInRange = false;

	void Update()
	{

		while(!isInRange)
			transform.Translate(Vector3.right * speed * Time.deltaTime);

	}
}

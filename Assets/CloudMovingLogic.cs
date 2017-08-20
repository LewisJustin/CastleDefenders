using UnityEngine;

public class CloudMovingLogic : MonoBehaviour
{
	[SerializeField] private GameObject toDestory;

	void Update()
	{
		transform.Translate(Vector3.right * Random.Range(5f, 6f) * Time.deltaTime);

		if (transform.position.x > 30)
			Destroy(toDestory);
	}
}

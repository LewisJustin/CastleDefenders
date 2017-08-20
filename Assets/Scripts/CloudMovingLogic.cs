using UnityEngine;

public class CloudMovingLogic : MonoBehaviour {
	
	void Update ()
	{
		transform.Translate(Vector3.right * Random.Range(.2f, .7f) * Time.deltaTime);

		if (transform.position.x > 100)
			Destroy(transform);
	}
}

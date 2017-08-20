using UnityEngine;

public class ViewScrolling : MonoBehaviour
{
	private void FixedUpdate()
	{
		if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
		{
			if(transform.position.x <= 10 && transform.position.x >= -10)
				transform.Translate(new Vector3 (-Input.GetAxis("Mouse ScrollWheel") * 500f * Time.deltaTime, 0f,0f));

			if (transform.position.x < -10)
				transform.Translate(new Vector3(.1f, 0f, 0f));

			if (transform.position.x > 10)
				transform.Translate(new Vector3(-.1f, 0f, 0f));
		}
	}
}

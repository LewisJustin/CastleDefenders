using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpScript : MonoBehaviour
{
	[SerializeField] Transform arrowPrefab;
	int arrowDensity;
	Vector3 firstArrowPosition;

	void Start()
	{
		arrowDensity = 30;
		firstArrowPosition = new Vector3(-15f, 10f, 0f);
	}
	void OnMouseOver()
	{
		for(int i = 0; i<arrowDensity; i++)
		{
			Instantiate(arrowPrefab, firstArrowPosition, transform.rotation);
			firstArrowPosition.x += 1f;
		}

		Destroy(gameObject);
	}
}
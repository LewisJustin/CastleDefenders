using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour {

	private float x = 180f;

	private void Update()
	{
		transform.rotation = Quaternion.Euler(0f,0f,x);

		x += .6f;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log("working");
	}
}

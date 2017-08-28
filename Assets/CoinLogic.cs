using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour {

	private GameObject gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager");
	}

	void OnMouseOver()
	{
		gameManager.GetComponent<GameLogic>().currency += 10;

		Destroy(gameObject);
	}
}

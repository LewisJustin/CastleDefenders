using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLogic : MonoBehaviour {

	[SerializeField] private Transform cloudSpawnPoints;
	[SerializeField] private Sprite cloudSprite;

	void Start ()
	{
		Transform _sp = cloudSpawnPoints;
		Instantiate(cloudSprite, _sp.position, _sp.rotation);
	}
}

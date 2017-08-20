using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLogic : MonoBehaviour {

	[SerializeField] private Transform[] cloudSpawnPoints;
	[SerializeField] private GameObject[] cloud;

	void Start ()
	{
		StartCoroutine(SpawnCloudsWaitTime());
	}

	IEnumerator SpawnCloudsWaitTime()
	{
		for(int i=0; i < 10; i++)
		{
			yield return new WaitForSeconds(1f);
			Transform _sp = cloudSpawnPoints[Random.Range(0, cloudSpawnPoints.Length)];
			Instantiate(cloud[Random.Range(0,cloud.Length)], _sp.position, _sp.rotation);
		}

	}
}

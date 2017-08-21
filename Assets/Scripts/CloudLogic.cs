using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLogic : MonoBehaviour
{

	[SerializeField] private Transform[] cloudSpawnPoints;
	[SerializeField] private GameObject[] cloudPrefabs;
    [SerializeField] private GameObject CloudHolder;
	[SerializeField] private float spawnRate;
    private GameObject cloud;

	void Start ()
	{
		StartCoroutine(SpawnCloudsWaitTime());
	}

	IEnumerator SpawnCloudsWaitTime()
	{
		for(int i=0; i < 10;)
		{
			yield return new WaitForSeconds(spawnRate);
			Transform _sp = cloudSpawnPoints[Random.Range(0, cloudSpawnPoints.Length)];
			cloud = Instantiate(cloudPrefabs[Random.Range(0,cloudPrefabs.Length)], _sp.position, _sp.rotation) as GameObject;
            cloud.transform.parent = CloudHolder.transform;
		}

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSystemPlayer : MonoBehaviour {

	void Start () 
	{
		this.gameObject.GetComponent<ParticleSystem>().Play(true);	
	}
}
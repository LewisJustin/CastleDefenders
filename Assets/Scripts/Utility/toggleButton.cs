using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class toggleButton : MonoBehaviour {

	[SerializeField] GameObject toToggle;

	public void toggleObject()
	{
		toToggle.SetActive(!toToggle.activeInHierarchy);
	}

}
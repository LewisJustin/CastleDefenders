using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class optionsScript : MonoBehaviour {

	[SerializeField] private GameObject optionsPanel;
	[SerializeField] private GameObject bow;
	[SerializeField] private GameObject audioSource;
	private bool tempAimingReversed;

	//Called when options button is
	//clicked from the menu panel
	public void OnOptionsOpenClick()
	{
		optionsPanel.SetActive(true);
	}

	public void OnOptionsCloseClick()
	{
		optionsPanel.SetActive(false);
	}

	public void ToggleAiming()
	{	
		bow.GetComponent<Rotate>().aimingReversed = !bow.GetComponent<Rotate>().aimingReversed;
	}

	public void OnVolumeSliderChanged(float newValue)
	{
		audioSource.GetComponent<AudioSource>().volume = newValue;
	}
}
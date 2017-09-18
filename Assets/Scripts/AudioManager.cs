using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	void Awake () {
		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public void OnVolumeChanged(float newVolume)
	{
		foreach(Sound s in sounds)
		{
			s.source.volume = newVolume;
		}
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s==null)
		{
			Debug.Log("No audio of name " + s.name)
			return;
		}
		s.source.Play();
	}
}
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	public static AudioManager audioManager;

	[HideInInspector] public float volume;

	void Awake () {
		if(audioManager == null)
			audioManager = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;
		}
	}

	public void OnVolumeChanged(float newVolume)
	{
		foreach(Sound s in sounds)
		{
			s.source.volume = newVolume;
		}
		volume = newVolume;
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s==null)
		{
			Debug.LogWarning("No audio of name " + name);
			return;
		}
		s.source.Play();
	}
}
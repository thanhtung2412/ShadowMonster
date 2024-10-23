using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;

	public static AudioManager instance;

	public bool muted;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		if (instance == null)
		{
			instance = this;
			Sound[] array = sounds;
			foreach (Sound sound in array)
			{
				sound.source = gameObject.AddComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.volume = sound.volume;
				sound.source.pitch = sound.pitch;
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == "wind");
		if (sound2 != null)
		{
			sound2.source.loop = true;
			sound2.source.playOnAwake = true;
			sound2.source.Play();
		}
	}

	public void Play(string name)
	{
		Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
		sound2?.source.PlayOneShot(sound2.clip);
	}

	public void Play2(string name)
	{
		Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
		if (sound2 != null && !sound2.source.isPlaying)
		{
			sound2.source.Play();
		}
	}

	public void SlowAudio()
	{
		Sound[] array = sounds;
		foreach (Sound sound in array)
		{
			if (!(sound.name == "slowmo") && !(sound.name == "slowmofix"))
			{
				sound.source.pitch = Time.timeScale;
			}
		}
	}

	public void Mute()
	{
		muted = !muted;
		if (muted)
		{
			Sound[] array = sounds;
			foreach (Sound sound in array)
			{
				sound.source = base.gameObject.GetComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.volume = 0f;
				sound.source.pitch = sound.pitch;
			}
		}
		else
		{
			Sound[] array = sounds;
			foreach (Sound sound2 in array)
			{
				sound2.source = base.gameObject.GetComponent<AudioSource>();
				sound2.source.clip = sound2.clip;
				sound2.source.volume = sound2.volume;
				sound2.source.pitch = sound2.pitch;
			}
		}
	}
}

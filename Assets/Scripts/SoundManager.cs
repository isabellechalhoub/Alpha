﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour 
{
	public AudioSource fxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
	public AudioClip menuSong;
	public AudioClip L1Song;
	public AudioClip L2Song;
	public AudioClip deathSong;
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.


	void Awake() 
	{
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy(gameObject);


		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		// play the game song
		if (instance.musicSource.clip != L1Song && instance.musicSource.clip != null) 
		{
			instance.musicSource.clip = L1Song;
			Cursor.visible = false;
		}
		else if(instance.musicSource.clip != L2Song && instance.musicSource.clip != null)
		{
			instance.musicSource.clip = L2Song;
			Cursor.visible = false;
		}
		// play the menu song
		else 
		{
			instance.musicSource.clip = menuSong;
			Cursor.visible = true;
		}
		instance.musicSource.Play();
	}

	void OnLevelWasLoaded() 
	{
		if (SceneManager.GetActiveScene().name == "TitleScreen") 
		{
			if(instance.musicSource.clip != menuSong) 
			{
				instance.musicSource.clip = menuSong;
				instance.musicSource.Play();
			}
			Cursor.visible = true;
		}
		else if (SceneManager.GetActiveScene().name == "L1")
		{
			if (instance.musicSource.clip != L1Song) 
			{
				instance.musicSource.clip = L1Song;
				instance.musicSource.Play();
			}
			Cursor.visible = false;
		}
		else if (SceneManager.GetActiveScene().name == "L2")
		{
			if (instance.musicSource.clip != L2Song) 
			{
				instance.musicSource.clip = L2Song;
				instance.musicSource.Play();
			}
			Cursor.visible = false;
		}
		instance.musicSource.Play ();
	}

	public void PlayDeathMusic()
	{
		instance.musicSource.clip = deathSong;
		instance.musicSource.Play();
	}

	//Used to play single sound clips.
	public void PlaySingle(AudioClip clip, float volume = 1f) {
		fxSource.PlayOneShot(clip, volume);
	}

	//RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
	public void RandomizeSfx(params AudioClip[] clips) {
		//Generate a random number between 0 and the length of our array of clips passed in.
		int randomIndex = Random.Range(0, clips.Length);

		//Choose a random pitch to play back our clip at between our high and low pitch ranges.
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		//Set the pitch of the audio source to the randomly chosen pitch.
		fxSource.pitch = randomPitch;

		//Set the clip to the clip at our randomly chosen index.
		fxSource.clip = clips[randomIndex];

		//Play the clip.
		fxSource.Play();
	}
}
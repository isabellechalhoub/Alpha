using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour 
{
	public AudioSource fxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
	public AudioClip menuSong;
	public AudioClip L1Song;
	public AudioClip L2Song;
	public AudioClip L3Song;
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
			//Cursor.visible = false;
		}
		else if(instance.musicSource.clip != L2Song && instance.musicSource.clip != null)
		{
			instance.musicSource.clip = L2Song;
			//Cursor.visible = false;
		}
		// play the menu song
		else 
		{
			instance.musicSource.clip = menuSong;
			//Cursor.visible = true;
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
			//Cursor.visible = true;
		}
		else if (SceneManager.GetActiveScene().name == "L1")
		{
			if (instance.musicSource.clip != L1Song) 
			{
				instance.musicSource.clip = L1Song;
				instance.musicSource.Play();
			}
			//Cursor.visible = false;
		}
		else if (SceneManager.GetActiveScene().name == "L2")
		{
			if (instance.musicSource.clip != L2Song) 
			{
				instance.musicSource.clip = L2Song;
				instance.musicSource.Play();
			}
			//Cursor.visible = false;
		}
        else if (SceneManager.GetActiveScene().name == "L3")
        {
            if (instance.musicSource.clip != L3Song)
            {
                instance.musicSource.clip = L3Song;
                instance.musicSource.Play();
            }
            //Cursor.visible = false;
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
}
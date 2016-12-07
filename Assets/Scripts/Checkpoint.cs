using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

	public static Checkpoint instance = null;     //Allows other scripts to call functions from SoundManager.
	public Vector3 spawn; 

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
		if (PlayerPrefs.GetInt ("Level") == 1)
			spawn = new Vector3 (-1542f, -1908.7f, 0);
		else if (PlayerPrefs.GetInt ("Level") == 2)
			spawn = new Vector3 (-952f, -3325f, 0);
		else if (PlayerPrefs.GetInt ("Level") == 3) {
			spawn = new Vector3 (-1594f, -1897.4f, 0);
		}
	}
	public void UpdateSpawn(Vector3 tr)
	{
		spawn = tr;
	}
}

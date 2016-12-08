using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	private bool check;
	public int level;

    // Use this for initialization
    void Start () 
	{
		PlayerPrefs.SetInt ("Level", level);
		resetUI ();
    }

	void resetUI()
	{
		PlayerPrefs.SetInt ("Heart1", 1);
		PlayerPrefs.SetInt ("Heart2", 1);
		PlayerPrefs.SetInt ("Heart3", 1);
		PlayerPrefs.SetInt ("Journal", 0);
		PlayerPrefs.SetInt ("Lunchbox", 0);
		PlayerPrefs.SetInt ("Jar", 0);
		PlayerPrefs.SetInt ("Plushie", 0);
		PlayerPrefs.SetInt ("Photo", 0);
		PlayerPrefs.SetInt ("Gameboy", 0);
		PlayerPrefs.SetInt ("Shell", 0);
	}

	// Event handling when Restart button clicked
	public void RestartLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Event handling when Exit button clicked - go to main menu
	public void ExitLevel()
	{
		resetUI ();
        SceneManager.LoadScene(0);
	}

	// Even handling for Exit game button on main menu
	public void ExitGame()
	{
		resetUI ();
		Application.Quit();
	}

	// Event handling for Play button on main menu - load level 1
	public void Play()
	{
		PlayerPrefs.SetInt ("Level", 1);
        SceneManager.LoadScene(1);
	}

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("L1"))
        {
            PlayerPrefs.SetInt("Level", 2);
            SceneManager.LoadScene(2);
        }
		else if (SceneManager.GetActiveScene().name.Equals("L2"))
        {
            PlayerPrefs.SetInt("Level", 3);
            SceneManager.LoadScene(3);
        }
    }
}

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
    }

	// Event handling when Restart button clicked
	public void RestartLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Event handling when Exit button clicked - go to main menu
	public void ExitLevel()
	{
        SceneManager.LoadScene(0);
	}

	// Even handling for Exit game button on main menu
	public void ExitGame()
	{
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
		PlayerPrefs.SetInt ("Level", 2);
        SceneManager.LoadScene(2);
    }
}

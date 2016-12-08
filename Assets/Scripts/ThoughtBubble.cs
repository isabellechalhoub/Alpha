using UnityEngine;
using System.Collections;


public class ThoughtBubble : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public Sprite lvl1;
    public Sprite patrol;
    public Sprite banjo;
	public Sprite jar;
	public Sprite lunchbox;
	public Sprite gopher;
	public float fadeTime;
	public float awakeTime;
	Color currColor;
	private bool In;
	private bool Out;

    // Use this for initialization
    void Start ()
    {
		currColor = gameObject.GetComponent<SpriteRenderer> ().color;
		currColor.a = 0;
		gameObject.GetComponent<SpriteRenderer> ().color = currColor;
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = player.transform.position + offset;
	}

    public void ChangeBubble(string sprite)
    {
		gameObject.SetActive (true);
        switch (sprite)
        {
			case "Level 1":
                gameObject.GetComponent<SpriteRenderer>().sprite = lvl1;
                break;
            case "Banjo":
                gameObject.GetComponent<SpriteRenderer>().sprite = banjo;
                break;
            case "Patrol":
                gameObject.GetComponent<SpriteRenderer>().sprite = patrol;
                break;
			case "Lunchbox":
				gameObject.GetComponent<SpriteRenderer>().sprite = lunchbox;
				break;
			case "Jar":
				gameObject.GetComponent<SpriteRenderer>().sprite = jar;
				break;
			case "Gopher":
				gameObject.GetComponent<SpriteRenderer>().sprite = gopher;
				break;
        }
		StartCoroutine (FadeIn());
    }

	public IEnumerator Wait(float time)
	{
		yield return new WaitForSecondsRealtime(time);
		StartCoroutine (FadeOut());
	}

	public IEnumerator FadeOut ()
	{
		currColor = gameObject.GetComponent<SpriteRenderer> ().color;
		while(currColor.a > 0)
		{
			currColor.a -= 0.01f;
			gameObject.GetComponent<SpriteRenderer> ().color = currColor;
			yield return new WaitForSeconds(fadeTime);
		} 
		gameObject.SetActive (false);
	}

	public IEnumerator FadeIn ()
	{
		currColor = gameObject.GetComponent<SpriteRenderer> ().color;
		while(currColor.a < 1)
		{
			currColor.a += 0.01f;
			gameObject.GetComponent<SpriteRenderer> ().color = currColor;
			yield return new WaitForSeconds(fadeTime);
		} 
		StartCoroutine (Wait (awakeTime));
	}
}

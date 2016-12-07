using UnityEngine;
using System.Collections;


public class ThoughtBubble : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public Sprite lvl1;
    public Sprite patrol;
    public Sprite banjo;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = player.transform.position + offset;
	}

    public void ChangeBubble(string sprite)
    {
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
        }
    }
}

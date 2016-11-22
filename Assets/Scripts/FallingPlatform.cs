using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float fallDelay;

    private BoxCollider2D player;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (player.IsTouching(gameObject.GetComponent<BoxCollider2D>()))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.isKinematic = false;
        yield return 0;
    }
}

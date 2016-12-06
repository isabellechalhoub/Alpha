using UnityEngine;
using System.Collections;
using Prime31;

public class PatrolEnemy : MonoBehaviour {

	public float speed = 1f;
	public float startingPos;
	public float endingPos;
    private int health = 7;
    private float distance;
	private Vector2 walking;
	public BoxCollider2D player;
	public PolygonCollider2D shield;
    public BoxCollider2D enemy;
    public GameObject me;
    public BoxCollider2D sword;

    void Start () 
	{
        distance = startingPos - endingPos;
		endingPos = transform.position.x - distance;
		startingPos = transform.position.x;
        //player = GameObject.FindGameObjectWithTag ("Player").GetComponent<BoxCollider2D> ();
        //shield = GameObject.FindGameObjectWithTag ("Shield").GetComponent<PolygonCollider2D> ();
        me = gameObject;
        enemy = me.GetComponent<BoxCollider2D>();
        //sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider2D>();
    }

	// Update is called once per frame
	void Update () 
	{
        if (health == 0)
        {
            me.SetActive(false);
        }

        if (transform.position.x > startingPos || transform.position.x < endingPos || enemy.IsTouching(player) || enemy.IsTouching(sword) || enemy.IsTouching(shield))
        {
            if (enemy.IsTouching(sword)) 
                {
                StartCoroutine(hit());
                StartCoroutine(Flash());
            }
            this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
        walking.x = -1 * speed * Time.deltaTime;
        transform.Translate (walking);
	}

    IEnumerator hit() 
    {
        health--;
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator Flash() {
        for (int i = 0; i < 2; i++) {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            yield return new WaitForSeconds(0.5f);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

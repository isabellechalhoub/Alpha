using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {

    public bool inSight = false;
    public bool inRange = false;
    public float speed = 1f;
    private int health = 3;
    private Vector2 walking;
    public BoxCollider2D player;
    public PolygonCollider2D shield;
    public BoxCollider2D enemy;
    public BoxCollider2D sword;
    public float sight;
    public float range;
    public LayerMask playerLayer;
    Vector3 movePos;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
		enemy = gameObject.GetComponent<BoxCollider2D>();
        enemy.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        inSight = Physics2D.OverlapCircle(transform.position, sight, playerLayer);
		inRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);
        if (inSight)
        {
            animator.SetBool("inRange", true);
        }
		if (inRange) {
			animator.SetBool ("attack", true);
			enemy.enabled = true;
		} 
		else 
		{
			animator.SetBool ("attack", false);
			enemy.enabled = false;
		}
        if (health == 0)
        {
			gameObject.SetActive(false);
        }
        if (enemy.IsTouching(sword))
        {
			StartCoroutine(hit());
			StartCoroutine(Flash());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, sight);
        Gizmos.DrawSphere(transform.position, range);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(sightStart.position, sightEnd.position);
    }

	IEnumerator hit() 
	{
		health--;
		yield return new WaitForSeconds(1.0f);
	}
	IEnumerator Flash() {
		for (int i = 0; i < 2; i++) {
			this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
			yield return new WaitForSeconds(0.5f);
			this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
			yield return new WaitForSeconds(0.5f);
		}
	}
}

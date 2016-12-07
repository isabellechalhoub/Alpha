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
    public GameObject me;
    public BoxCollider2D sword;
    public float sight;
    public float range;
    public LayerMask playerLayer;
    Vector3 movePos;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        //shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<PolygonCollider2D>();
        me = gameObject;
        animator = gameObject.GetComponent<Animator>();
        enemy = me.GetComponent<BoxCollider2D>();
        enemy.enabled = false;
        //sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        inSight = Physics2D.OverlapCircle(transform.position, sight, playerLayer);
        if (inSight)
        {
            animator.SetBool("inRange", true);
            //movePos.x = player.transform.position.x;
           // movePos.z = transform.position.z;
           // movePos.y = transform.position.y;
            //transform.position = Vector3.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
        }
        if (inRange)
        {
            animator.SetBool("attack", true);
            enemy.enabled = true;
        }
        if (health == 0)
        {
            me.SetActive(false);
        }
        if (enemy.IsTouching(sword))
        {
            health--;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, sight);
        Gizmos.DrawSphere(transform.position, range);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(sightStart.position, sightEnd.position);
    }
}

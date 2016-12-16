using UnityEngine;
using System.Collections;

public class ShootingEnemy : MonoBehaviour {

    public LayerMask playerLayer;
    public bool inRange;
    public int health;
    public float range;
    private bool canShoot;
    private Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private Vector3 newPosition;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        bulletSpawn = gameObject.transform;
        canShoot = true;
        animator = gameObject.GetComponent<Animator>();
        newPosition = new Vector3((bulletSpawn.position.x + 0.5f), (bulletSpawn.position.y + 1.5f), bulletSpawn.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health == 0)
        {
            StopCoroutine(Shoot());
            gameObject.SetActive(false);
        }

        inRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (inRange)
        {
            if (canShoot)
                StartCoroutine(Shoot());
            animator.SetBool("shoot", true);
        }
        else
        {
            animator.SetBool("shoot", false);
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        StartCoroutine(disable());

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, newPosition, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * (-1*bulletSpeed), ForceMode2D.Impulse);
    
        Destroy(bullet, 5.0f);

        yield return new WaitForSeconds(2.5f);

        canShoot = true;
    }

    IEnumerator disable()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("bullet"))
        {
            Destroy(col.gameObject);
            StartCoroutine(Flash());
            health--;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, range);
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < 2; i++)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

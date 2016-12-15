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

    // Use this for initialization
    void Start ()
    {
        bulletSpawn = gameObject.transform;
        canShoot = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health == 0)
        {
            StopCoroutine(Shoot());
            gameObject.active = false;
        }

        inRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (inRange && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * (-1*bulletSpeed), ForceMode2D.Impulse);
    
        Destroy(bullet, 5.0f);

        yield return new WaitForSeconds(2.5f);

        canShoot = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.tag.Equals("bullet"))
        //{
        //    health--;
        //}
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, range);
    }
}

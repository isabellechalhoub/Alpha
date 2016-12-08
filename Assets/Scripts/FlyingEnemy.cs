using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float range;
    public LayerMask playerLayer;
    public bool inRange;
	public int health;
	private AnimationController2D _anim_control;
	private BoxCollider2D enemy;
	public BoxCollider2D sword;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		_anim_control = gameObject.GetComponent<AnimationController2D> ();
		enemy = gameObject.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        inRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);

		if (inRange) 
		{
			if (transform.position.x < player.transform.position.x) 
				_anim_control.setFacing ("Left");
			else
				_anim_control.setFacing ("Right");
			
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
		}

		if (enemy.IsTouching(sword))
		{
			Debug.Log ("hit");
			StartCoroutine(hit());
			StartCoroutine(Flash());
		}

		if (health == 0) 
		{
			gameObject.SetActive (false);
		}
	}

    void OnDrawGizmosSelected ()
    {
        Gizmos.DrawSphere(transform.position, range);
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

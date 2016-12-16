using UnityEngine;
using System.Collections;

public class GateLogic : MonoBehaviour 
{
	public Rigidbody2D rbody;
	public float speed;
	private bool rotate;

	// Use this for initialization
	void Start ()
	{
		rotate = false;
		rbody = this.GetComponent<Rigidbody2D>();
	}
		
	public void startRotate()
	{
		rotate = true;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (rotate)
		{
			//rbody.MoveRotation(rbody.rotation + speed * Time.fixedDeltaTime);
			transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
			if (transform.rotation.eulerAngles.y >= 180) 
			{
				rotate = false;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TurnOff()
	{
		BoxCollider2D[] colls = gameObject.GetComponentsInChildren<BoxCollider2D> ();
		foreach(BoxCollider2D col in colls)
		{
			col.enabled = false;
		}
	}
}

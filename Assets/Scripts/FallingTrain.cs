using UnityEngine;
using System.Collections;

public class FallingTrain : MonoBehaviour
{
    public float fallSpeed;
    public LayerMask platformMask = 0;
    private bool fall;

    // Use this for initialization
    void Start ()
    {
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

	public void SetKinematic(bool kin)
	{
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = kin;
	}
}

using UnityEngine;
using System.Collections;

public class FallingTrain : MonoBehaviour
{
    //public SpawnManager sm;

    // Use this for initialization
    void Start ()
    {
        //sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.transform.position.y < -658)
        {
            Debug.Log("here");
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, 44f, 0f);
        }
        //StartCoroutine(spawn());
    }

    //public IEnumerator spawn()
    //{
    //    GameObject train = sm.GetFromPool(transform.position);
    //    train.GetComponent<Rigidbody2D>().velocity = transform.up * -4;
    //    yield return new WaitForSecondsRealtime(10f);
    //}

    //public IEnumerator Destroy(GameObject go, float timer)
    //{
    //    yield return new WaitForSeconds(10f);
    //    sm.UnSpawnObject(go);
    //}

    public void SetKinematic(bool kin)
	{
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = kin;
	}
}

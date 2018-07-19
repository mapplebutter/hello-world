using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {

    public GameObject Prefab;

    public string objectTag = "";
    bool hasPrefab = false;


    float timer = 0.0f;
    public int penalty = 5;

    public GameObject refPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!hasPrefab)
        {
            if (timer > 0.5f)
            {
                Instantiate(Prefab, refPoint.transform.position, transform.rotation);
                timer = 0;
                ScoreCheck.instance.Deductscore(penalty);
                hasPrefab = true;
            }
            else timer += Time.deltaTime;
        }
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == objectTag) hasPrefab = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == objectTag) hasPrefab = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == objectTag) hasPrefab = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == objectTag)
        {
            hasPrefab = false;
        }
    }
}

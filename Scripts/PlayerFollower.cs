using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public GameObject player, cameraRig;
    public Vector3 OriginalPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            cameraRig.transform.position = new Vector3(OriginalPos.x, cameraRig.transform.position.y,OriginalPos.z);
        }
    }
}

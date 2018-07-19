using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActive : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
        Invoke("DelayedStart", 2.0f);
	}

    void DelayedStart()
    {
        target.SetActive(true);
    }
}

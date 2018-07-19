using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkSnap : MonoBehaviour {

    public GameObject snapped, notSnapped;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Snapped()
    {
        snapped.SetActive(true);
        notSnapped.SetActive(false);
    }

    public void Unsnapped()
    {
        snapped.SetActive(false);
        notSnapped.SetActive(true);
    }
}

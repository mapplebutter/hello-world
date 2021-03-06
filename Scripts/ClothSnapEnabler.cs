﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothSnapEnabler : MonoBehaviour {

    GameObject parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.parent)
        {
            parent = gameObject.transform.parent.gameObject;

            while(parent.transform.parent != null)
            {
                parent = parent.transform.parent.gameObject;
            }

        }
        
        if (parent != null && parent.tag == "Plate")
        {
            for (int i = 0; i < 3; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < 3; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key_Anim : MonoBehaviour {

    private Image image;

    bool Full = false;

	// Use this for initialization
	void Start ()
    {
        image = GetComponent<Image>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Full == false)
        {
            image.fillAmount -= 1f * Time.deltaTime;
            if (image.fillAmount == 0)
            {
                Full = true;
            }
        }
        else
        {
            image.fillAmount += 1f * Time.deltaTime;
            if (image.fillAmount == 1)
            {
                Full = false;
            }
        }
    }
}

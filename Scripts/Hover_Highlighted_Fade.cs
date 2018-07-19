using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hover_Highlighted_Fade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    bool highlighted = false;

    [SerializeField]
    private Image image;

    // Use this for initialization
    void Start ()
    {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (highlighted == true)
        {
            image.fillAmount -= 1f * Time.deltaTime;
            if(image.fillAmount <= 0)
            {
                image.fillAmount = 1;
            }
        }
        else image.fillAmount = 1f;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlighted = false;
    }
}

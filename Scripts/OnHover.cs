using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool isTrue = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isTrue = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isTrue = false;
    }
}

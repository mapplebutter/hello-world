using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeightAdjust : MonoBehaviour {

    public GameObject sliderHandler;
    Vector3 originalPosition = Vector3.zero;
    public GameObject playerPos;
    public float HeightFactor = 0.2f;

    public Slider slider;
    public float sliderValue = 0.5f;

    // Use this for initialization
    void Start () {
        originalPosition = playerPos.transform.position;
        playerPos.transform.position = new Vector3(playerPos.transform.position.x, originalPosition.y - (1 * HeightFactor) + slider.value * HeightFactor, playerPos.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (sliderHandler.GetComponent<OnHover>().isTrue)
            sliderValue = slider.value;
        else
            slider.value = sliderValue;
	}

    public void ChangePlayerHeight(float value)
    {
        if (sliderHandler.GetComponent<OnHover>().isTrue)
            playerPos.transform.position = new Vector3(playerPos.transform.position.x, originalPosition.y - (1 * HeightFactor) + value * HeightFactor, playerPos.transform.position.z);
    }
}

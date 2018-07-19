using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardTextColor : MonoBehaviour {

    private GameObject[] keyBoardText;
    public Color keyBoardTextColor;

    private GameObject[] numberPadText;
    public Color numberPadTextColor;

    private GameObject[] buttonsImage;
    public Color buttonsImageColor;

    private GameObject inputText;
    public Color inputTextColor;

    private GameObject[] canvasUI;
    public Camera eventCamera;

    // Use this for initialization
    void Start()
    {
        //Find all game objects with tag "KeyboardText" and set color with new color
        keyBoardText = GameObject.FindGameObjectsWithTag("KeyboardText");
        foreach (GameObject keyBoardAlphabets in keyBoardText)
        {
            keyBoardAlphabets.GetComponent<Text>().color = keyBoardTextColor;
        }

        //Find all game objects with tag "NumberpadText" and set color with new color
        numberPadText = GameObject.FindGameObjectsWithTag("NumberpadText");
        foreach(GameObject numberPadKeys in numberPadText)
        {
            numberPadKeys.GetComponent<Text>().color = numberPadTextColor;
        }

        //Find all game objects with tag "ButtonsImage" and set color with new color
        buttonsImage = GameObject.FindGameObjectsWithTag("ButtonsImage");
        foreach(GameObject buttonsImg in buttonsImage)
        {
            buttonsImg.GetComponent<Image>().color = buttonsImageColor;
        }

        //Find all game objects with tag "CanvasUI" and event camera
        canvasUI = GameObject.FindGameObjectsWithTag("CanvasUI");
        foreach(GameObject canvas in canvasUI)
        {
            canvas.GetComponent<Canvas>().worldCamera = eventCamera;
        }

        //Find game object with tag "InputText" and set color with new color
        inputText = GameObject.FindGameObjectWithTag("InputText");
        inputText.GetComponent<Text>().color = inputTextColor;           
    }    
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SetUIColor : MonoBehaviour {    
    
    //Assessment
    private GameObject[] assessmentBackgrounds;
    private GameObject[] assessmentTexts;
    public Color assessmentBackground;
    public Color assessmentsText;    

    //Keyboard
    private GameObject[] keyboardBackgroundImage;
    private GameObject[] keyboardKeysText;
    private GameObject[] keyboardNumberText;
    private GameObject[] keyboardButtonImage;
    private GameObject keyboardBackspaceImage;
    private GameObject keyboardEnterImage;

    [Space(20)]
    public Color keyboardBackgrounds;
    public Color keyboardKeys;
    public Color keyboardNumbers;
    public Color keyboardButtons;
    public Color keyboardBackspace;
    public Color keyboardEnter;

	// Use this for initialization
	private void Awake ()
    {
        //Assessment
        assessmentBackgrounds = GameObject.FindGameObjectsWithTag("AssessmentBackground");
        assessmentTexts = GameObject.FindGameObjectsWithTag("AssessmentText");

        //Keyboard
        keyboardBackgroundImage = GameObject.FindGameObjectsWithTag("KeyboardBackground");
        keyboardKeysText = GameObject.FindGameObjectsWithTag("KeyboardText");
        keyboardNumberText = GameObject.FindGameObjectsWithTag("NumberpadText");
        keyboardButtonImage = GameObject.FindGameObjectsWithTag("KeyboardButtons");
        keyboardBackspaceImage = GameObject.FindGameObjectWithTag("KeyboardBackspace");
        keyboardEnterImage = GameObject.FindGameObjectWithTag("KeyboardEnter");    
    }    

    // Update is called once per frame
    private void Update ()
    {
        Assessment();

        Keyboard();    
    }

    private void Assessment()
    {
        //Assessment
        foreach (GameObject backgrounds in assessmentBackgrounds)
        {
            backgrounds.GetComponent<Image>().color = assessmentBackground;
        }

        foreach (GameObject texts in assessmentTexts)
        {
            texts.GetComponent<Text>().color = assessmentsText;
        }
    }
    private void Keyboard()
    {
        //Keyboard
        foreach (GameObject keyboardBackground in keyboardBackgroundImage)
        {
            keyboardBackground.GetComponent<Image>().color = keyboardBackgrounds;
        }

        foreach (GameObject keyText in keyboardKeysText)
        {
            keyText.GetComponent<Text>().color = keyboardKeys;
        }

        foreach (GameObject keyboardNumber in keyboardNumberText)
        {
            keyboardNumber.GetComponent<Text>().color = keyboardNumbers;
        }

        foreach (GameObject keyboardButton in keyboardButtonImage)
        {
            keyboardButton.GetComponent<Image>().color = keyboardButtons;
        }
    }
}

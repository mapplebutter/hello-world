using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public Image finishButtonImage;
    public Image finishButtonUIBar;
    public Sprite redKey;
    public Sprite greenKey;
    public Sprite uiBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFinishButton(Button endGame)
    {
        if(endGame.name == "GuidedModeButton")
        {
            finishButtonImage.sprite = greenKey;
            finishButtonUIBar.sprite = uiBar;
        }

        else if(endGame.name == "TestModeButton")
        {
            finishButtonImage.sprite = redKey;
            finishButtonUIBar.sprite = uiBar;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{

    public InputField input;
    private bool nameEntered = false;
    public GameObject howToPlayScreen;
    public AudioSource keyboardAudio;


    public void ClickKey(string character)
    {
        input.text += character;
    }

    public void Backspace()
    {
        if (input.text.Length > 0)
        {
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }

    public void ClearAll()
    {
        input.text = "";
    }
    public void Enter()
    {
        howToPlayScreen.SetActive(true);
        TitleMenu.instance.canMove = true;
        if(input.text.Length == 0)
        {
            input.text = "Steve";

        }

        GameManager.instance.playerName = input.text;
        input.text = "";
        nameEntered = true;
        TitleMenu.instance.LoadGameMode();
        gameObject.SetActive(false);        
    }

    private void Start()
    {
        input = GetComponentInChildren<InputField>();
    }

    public bool HasNameBeenEntered()
    {
        return nameEntered;
    }

    public void PlayKeyboardAudio()
    {
        keyboardAudio.Play();
    }
}

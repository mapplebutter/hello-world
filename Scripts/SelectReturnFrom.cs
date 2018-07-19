using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectReturnFrom : MonoBehaviour {



    public void SelectPlay()
    {
        gameObject.GetComponent<Animator>().SetBool("OpenPlay", true);        
    }

    public void SelectOptions()
    {
        gameObject.GetComponent<Animator>().SetBool("OpenOptions", true);
    }

    public void OpenKeyboard()
    {
        gameObject.GetComponent<Animator>().SetBool("OpenKeyboard", true);        
    }

    public void OpenLeaderboard()
    {
        gameObject.GetComponent<Animator>().SetBool("OpenLeaderboard", true);
    }

    public void ReturnFromSelected()
    {
        if(gameObject.GetComponent<Animator>().GetBool("OpenPlay") == true && gameObject.GetComponent<Animator>().GetBool("OpenKeyboard") == false)
        {
            gameObject.GetComponent<Animator>().SetBool("OpenPlay", false);
        }
        else if(gameObject.GetComponent<Animator>().GetBool("OpenPlay") == true && gameObject.GetComponent<Animator>().GetBool("OpenKeyboard") == true)
        {
            gameObject.GetComponent<Animator>().SetBool("OpenKeyboard", false);
        }

        if (gameObject.GetComponent<Animator>().GetBool("OpenOptions") == true)
        {
            gameObject.GetComponent<Animator>().SetBool("OpenOptions", false);
        }

        if(gameObject.GetComponent<Animator>().GetBool("OpenLeaderboard") == true)
        {
            gameObject.GetComponent<Animator>().SetBool("OpenLeaderboard", false);
        }
    }
}

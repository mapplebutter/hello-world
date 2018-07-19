using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMode : MonoBehaviour
{
    /// <summary>
    /// This script controls the appearance of UI objects in the guided mode as well as the appearance of the grid guide
    /// </summary>

    public GameObject howToPlayScreen;
    public GameObject[] HelpScreens = new GameObject[7];
    public static GuidedMode instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (string name in GameSaveLoad.GetNames())
        {
            if (name == GameManager.instance.playerName)
            {
                return;
            }
        }
    }
}

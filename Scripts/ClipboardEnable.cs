using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ClipboardEnable : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GameObject clipboard;


	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && TitleMenu.instance.gameMode == TitleMenu.Mode.Guided)
            EnableClipBoard();

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && TitleMenu.instance.gameMode == TitleMenu.Mode.Guided)
            DisableClipBoard();

	}

    void EnableClipBoard()
    {
        clipboard.SetActive(true);
    }

    void DisableClipBoard()
    {
        clipboard.SetActive(false);
    }
}

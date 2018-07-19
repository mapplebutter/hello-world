using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuEnable : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GameObject pauseMenu, refPoint;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && TitleMenu.instance.gameMode != TitleMenu.Mode.empty)
        {
            if (pauseMenu.activeInHierarchy == false)
                EnablePause();
            else if (pauseMenu.activeInHierarchy == true)
                DisablePause();
        }
	}

    void EnablePause()
    {
        pauseMenu.transform.parent = refPoint.transform;
        pauseMenu.transform.position = refPoint.transform.position;
        pauseMenu.transform.localRotation = Quaternion.identity;
        pauseMenu.SetActive(true);
        pauseMenu.GetComponent<PauseMenu>().isPaused = true;
    }

    void DisablePause()
    {
        pauseMenu.SetActive(false);
        pauseMenu.transform.parent = null;
        pauseMenu.GetComponent<PauseMenu>().isPaused = false;
    }
}

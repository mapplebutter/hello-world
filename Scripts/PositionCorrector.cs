using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// This script corrects the player's y position when the play area's transform is too far below the level.
/// This fixes the issue of the curved UI mesh pushing the player through the floor at the start of the game.
/// </summary>

public class PositionCorrector : MonoBehaviour {

    private Transform playArea;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (playArea == null)
        {
            playArea = VRTK_DeviceFinder.PlayAreaTransform();
        }
        else
        {
            if (playArea.position.y <= -1.0f)
            {
                playArea.position = new Vector3(5f, -0.441f, 3f);
            }
        }
	}
}

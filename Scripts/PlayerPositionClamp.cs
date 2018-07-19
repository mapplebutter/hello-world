using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionClamp : MonoBehaviour
{

    public Transform playerPos;
    //This is under the assumption that using the VRTK to move will be able to walk pass a mathf clamp so we will
    //use this to make sure the player doesn't walk out from where we want the player to.

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.x > 4.5f)
        {
            if (playerPos.localPosition.z > 7.0f)
            {
                return;
            }
            else
            {
             playerPos.position = new Vector3(3.0f, playerPos.position.y, playerPos.position.z);
            }

        }
        if (transform.localPosition.x < -10.2f)
        {
            playerPos.position = new Vector3(-10.2f, playerPos.position.y, playerPos.position.z);
        }

        if (transform.localPosition.z > 14.1f)
        {
            playerPos.position = new Vector3(playerPos.position.x, playerPos.position.y, 14.1f);
        }
        if (transform.localPosition.z < -3.0f)
        {
            playerPos.position = new Vector3(playerPos.position.x, playerPos.position.y, -3.0f);
        }
    }
}

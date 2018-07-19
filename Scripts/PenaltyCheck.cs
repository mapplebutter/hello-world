using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyCheck : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)ObjectLayer.Penalty)
        {
            // increase penality count function
            ScoreCheck.instance.penaltyCount++;
        }
    }
}

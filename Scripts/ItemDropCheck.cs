using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ItemDropCheck : MonoBehaviour {

    /// <summary>
    /// This script is attached to the room object
    /// It checks whether any object is dropped/thrown by the player and deducts 5 marks for each object
    /// If the item is breakable, an additional 5 marks is deducted in the object breaking script
    /// </summary>

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Utensils")
        {
            //if the gameobject has been dropped before, do not continue deducting marks until it has been picked up off the floor
            if (!collision.gameObject.GetComponent<InteractableObj>().isDropped)
            {
                collision.gameObject.GetComponent<InteractableObj>().isDropped = true;
                ScoreCheck.instance.Deductscore(5);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Utensils")
        {
            if(collision.gameObject.GetComponent<InteractableObj>().isDropped)
            {
                collision.gameObject.GetComponent<InteractableObj>().isDropped = false;
            }
        }
    }
}

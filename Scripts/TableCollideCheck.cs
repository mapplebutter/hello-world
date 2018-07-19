using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TableCollideCheck : MonoBehaviour {

    /// <summary>
    /// This script checks objects that touch the table
    /// If the object that touches the table is a tray/dinner plate, deduct 2 marks each time
    /// </summary>
    public static TableCollideCheck instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    int contactPenalty = 0;

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.StartTestTimer();
        //if the collider belongs to the serving cloth, dinner plate or tray, deducts marks accordingly
        if(collision.gameObject.tag == "Tray" || collision.gameObject.tag == "Plate" || collision.gameObject.tag == "Cloth")
        {
            if(!collision.gameObject.GetComponent<InteractableObj>().isTouchingTable)
            {
                collision.gameObject.GetComponent<InteractableObj>().isTouchingTable = true;
                contactPenalty = 2;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if the collider is moved away from the table
        if (collision.gameObject.tag == "Tray" || collision.gameObject.tag == "Plate" || collision.gameObject.tag == "Cloth")
        {
            if (collision.gameObject.GetComponent<InteractableObj>().isTouchingTable)
            {
                collision.gameObject.GetComponent<InteractableObj>().isTouchingTable = false;
            }
        }
    }

    public int getContactPenalty
    {
        get { return contactPenalty; }
        set { contactPenalty = value; }
    }
}

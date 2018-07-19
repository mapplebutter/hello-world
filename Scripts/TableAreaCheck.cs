using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableAreaCheck : MonoBehaviour
{

    //In this script, objects will be checked using colliders 

    public GameObject[] utensils;
    public GameObject[] otherObjects;
    public GameObject[] clothKnife;
    Collider[] temp;

    void Start()
    {
        utensils = new GameObject[3];
        otherObjects = new GameObject[3];
    }

    void CheckItems()
    {
        //WallTableArea is just an empty gameobject with a tag, the position will be used.
        if (this.gameObject.tag == "WallTableArea")
        {
            //Using physics overlapbox to find all the colliders in that area and store them temporarily
            temp = Physics.OverlapBox(this.gameObject.transform.position, new Vector3(0.7f, 0.1f, 0.2f), Quaternion.identity);
        }
        else
        {
            temp = Physics.OverlapBox(this.gameObject.transform.position, new Vector3(0.2f, 0.1f, 0.7f), Quaternion.identity);
        }

        int counter = 0;
        int counter2 = 0;
        int counter3 = 0;

        foreach (Collider c in temp)
        {
            //Check through all the colliders and put in the items into their respective GameObject array
            if (c.gameObject.tag == "Utensils")
            {
                if (counter >= 3)
                {
                    //Skip
                }
                else
                {
                    utensils[counter] = c.gameObject;

                    //After adding the above, specifically checked for the dinnerknife into another array
                    if (c.gameObject.transform.Find("UtensilType").tag == "DinnerKnife")
                    {
                        clothKnife[0] = c.gameObject;
                    }
                    counter++;
                }
            }
            else if (c.gameObject.tag == "SidePlates")
            {
                if (counter2 >= 1)
                {
                    //Skip
                }
                else
                {
                    otherObjects[counter2] = c.gameObject;
                    counter2++;
                }
            }
            else if(c.gameObject.tag =="Napkin")
            {
                clothKnife[1] = c.gameObject;
            }
        }
    }
}

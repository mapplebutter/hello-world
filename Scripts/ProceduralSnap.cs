using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ProceduralSnap : MonoBehaviour {
    /// <summary>
    /// This class controls the activation of snap zones by only making certain snap zones appear 
    /// after previous snap zones have been filled
    /// </summary>

    public int snapCount = 0; //tracks the number of objects that have been snapped in the stack
    private int unsnapCount = 0;
    public int snapMaxCount;
    public VRTK_SnapDropZone[] snapZones = new VRTK_SnapDropZone[9]; //the zones listed in the order they should be snapped by
    private bool hasBeenFilled = false; //returns true if the dinner plate has been filled with all the objects and false if not

    private void Start()
    {
        for (int i = 0; i < snapZones.Length; i++)
        {
            snapZones[i].proceduralSnapPos = i;
        }
    }

    private void Update()
    {

        if (snapZones[snapCount].GetComponent<VRTK_SnapDropZone>().GetCurrentSnappedObject() != null) //if there is something snapped to the current snap zone
        {
            if (snapCount < 8)
            {
                snapZones[snapCount + 1].gameObject.SetActive(true); //enable the snap zone that comes after it
            }

            for (int i = 8; i > snapCount + 1; i--)
            {
                snapZones[i].gameObject.SetActive(false); //disable the snapzones on top except the one that comes after
            }
        }

        //if (snapCount > 0) //exclude the first snapzone
        //{
        //    //for (int i = snapCount + 1; i < 8; i++)
        //    //{
        //    //    snapZones[i].gameObject.SetActive(false); //disable the snapzones on top except the one that comes after
        //    //}

            
        //}
    }


    /// <summary>
    /// Updates the snap count every time a new object is snapped.
    /// This function is called in the snap zone scripts whenever the object is snapped in the valid order
    /// </summary>
    public void UpdateSnapCount()
    {
        if (snapCount < 8)
        {
            snapCount++;
            snapZones[snapCount].gameObject.SetActive(true);
        }

        if(snapCount == 8)
        {
            hasBeenFilled = true;
        }
    }

    /// <summary>
    /// Updates the unsnap count every time an object is unsnapped.
    /// This function is called in the snap zone scripts when the object being unsnapped is in the valid order
    /// </summary>
    public void UpdateUnsnapCount()
    {
        if (hasBeenFilled)
        {
            snapCount--;
            hasBeenFilled = false;
        }
        else
        {
            snapZones[8].gameObject.SetActive(false);
            snapZones[snapCount].gameObject.SetActive(false);
            snapCount--;
        }
        if(snapCount < 0)
        {
            snapCount = 0;
            snapZones[0].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Checks if the snap zone is in the correct order of objects to be unsnapped
    /// If it the dinner plate has not been filled with all the necessary objects: 
    /// if it is beneath the highest object in the stack, do not allow unsnapping of the object
    /// else if the dinner plate has been filled with all the necessary objects:
    /// if it is in the wrong unsnap order, do not allow unsnapping of the object
    /// </summary>
    /// <param name="sz"></param>
    /// <returns>true, false</returns>
    
    public bool IsValidSnapZone(VRTK_SnapDropZone sz)
    {
        //if the snap zone is not at the top of the stack
        //i.e. it is not the last object in the array (same as snapCount),
        //Do not allow the object to be unsnapped
        if(snapCount == sz.proceduralSnapPos)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// returns true if the dinner plate has been filled with all the objects
    /// and false if the dinner plate is not filled/only partially filled
    /// </summary>
    public bool HasBeenFilled
    {
        get { return hasBeenFilled; }
    }
}

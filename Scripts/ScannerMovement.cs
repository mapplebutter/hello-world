using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerMovement : MonoBehaviour {

    public GameObject End;
    public List<GameObject> thisUtensilsList;

    void Update()
    {
        Vector3 thisPosition = this.gameObject.transform.position;

        thisPosition = Vector3.MoveTowards(thisPosition, End.transform.position, 1.0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Utensils")
        {
            //Objects will be added from left to right
            thisUtensilsList.Add(col.gameObject);
        }
    }
}

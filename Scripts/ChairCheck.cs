using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairCheck : MonoBehaviour {

    // Assign ChairSeries GameObject array when spawned chairs
    // Assign Table GameObject

    public GameObject[] ChairSeries = new GameObject[4];

    public GameObject Table;
    bool[] takenSeries = new bool[4] {false,false,false,false};

    int ScoreToDeduct = 0;

    public float acceptableOffset = 0.1f;
    public float acceptableDistance = 0.2f;

    float distanceToCompare = 0;
    int equalDistanceCounter = 0;

    public int CheckChair()
    {

        ScoreToDeduct = 0;
        equalDistanceCounter = 0;
        distanceToCompare = 0;
        equalDistanceCounter = 0;
        

        //acceptable distance
        for (int i = 0; i < 4; i++)
        {
            bool isCorrect = CheckAcceptableDistance(i);
            if (!isCorrect) ScoreToDeduct++;
        }

        //equal distance
        for (int i = 0; i < 4; i++)
        {
            if (CheckEqualDistance(i)) equalDistanceCounter++;
        }

        if (equalDistanceCounter < 4)
        {
            ScoreToDeduct++;
        }
        return ScoreToDeduct;
    }

    bool CheckAcceptableDistance(int index) // Check if end of the distance between end of table to the back of chair is within the acceptable range
    {
        float smallestChairDistance = 0;
        int tableindex = 0;

        for (int i = 0; i < 4; i++)
        {
            float chairDistance = Vector3.Distance(ChairSeries[index].transform.GetChild(1).transform.position, Table.transform.GetChild(i + 5).transform.GetChild(2).transform.position);
            if (smallestChairDistance == 0 || smallestChairDistance > chairDistance && takenSeries[i] == false)
            {
                smallestChairDistance = chairDistance;
                tableindex = i;
            }

        }

        takenSeries[tableindex] = true;

        smallestChairDistance = Mathf.Abs(smallestChairDistance);

        if (smallestChairDistance <= acceptableDistance + acceptableOffset && smallestChairDistance >= acceptableDistance - acceptableOffset) return true;
        else
        {
            return false;
        }
    }

    bool CheckEqualDistance(int index) // Check if the distance between each chair and the table is around the same distance
    {
        float distance = Vector3.Distance(ChairSeries[index].transform.GetChild(1).transform.position, Table.transform.position);
        distance = Mathf.Abs(distance);

        if (index == 0) distanceToCompare = distance;

        if (distance <= distanceToCompare + acceptableOffset && distance >= distanceToCompare - acceptableOffset)
            return true;
        else
            return false;
    }
}

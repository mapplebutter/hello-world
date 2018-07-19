using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CorrectUtensilChecker : MonoBehaviour
{
    public static CorrectUtensilChecker instance = null;

    public bool DinnerKnife = false, ButterKnife = false, DinnerFork = false;
    int CorrectCount = 0;

    void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public bool DKnife
    {
        get { return DinnerKnife; }
        set { DinnerKnife = value; }
    }

    public bool BKnife
    {
        get { return ButterKnife; }
        set { ButterKnife = value; }
    }

    public bool DFork
    {
        get { return DinnerFork; }
        set { DinnerFork = value; }
    }

    public int Count()
    {
        if (DinnerKnife == true)
        {
            CorrectCount++;
        }
        if (ButterKnife == true)
        {
            CorrectCount++;
        }
        if (DinnerFork == true)
        {
            CorrectCount++;
        }
        if(CorrectCount>4)
        {
            CorrectCount = 4;
        }
        return CorrectCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScoreCheck : MonoBehaviour
{
    public static ScoreCheck instance = null;
    public int score = 0;

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

    //For first check - 1.5cm +/-0.3 from edge of table
    float maxUtensilRotation = 0.0f;
    float totalUtensilRotation; //Will have average later
    bool consistent;

    public GameObject table;
    public GameObject[] Areas;
    public GameObject[] walls;
    GameObject[,] utensils;
    GameObject[] wrongObjects;

    //For second check - Distance between object for the plate
    GameObject dinnerKnife;
    GameObject dinnerFork;

    //For third check - If butter knife side is align with inner side plate & top and bottom same length not touching plate
    GameObject sidePlate;
    GameObject butterKnife;

    //For fourth check - If bottom, stem of wine glass is 1.5cm away from the top of dinner knife
    GameObject tableMiddle;
    GameObject[] dinnerKnifes;

    //For sixth check - Check salt, peppermill position to bar
    GameObject salt;
    GameObject pepper;
    GameObject longTable;

    public GameObject AssessmentBoard;
    public int penaltyCount = 0;
    public int PositionMarks = 0;

    //Seventh check - for Knife on napkin

    public void StartReferencing()
    {
        AssessmentBoard = GameObject.Find("AssessmentCanvas");
    }
    private void Start()
    {
        utensils = new GameObject[4, 3];
    }

    public void StartChecking()
    {
        longTable = GameObject.Find("longTable");
        FirstCheck();
        SecondCheck();
        ThirdCheck();
        FourthCheck();
        FifthCheck();
        SixthCheck();
    }

    void FirstCheck()
    {
        //This checks the 1.5cm of the 3 utensils from the edge of table. + 0.3 margin error
        //check if which wall then check which vector (1.2cm == 0.012m)
        bool deducted = false;

        for (int i = 0; i < 4; i++)
        {
            for (int x = 0; x < 3; x++)
            {
                //For each side of the table, for each utensil
                if (Areas[i].GetComponent<TableAreaCheck>().utensils[x] != null)
                {


                    utensils[i, x] = Areas[i].GetComponent<TableAreaCheck>().utensils[x];

                    if (utensils[i, x] != null)
                    {
                        if (TableFunc(walls[i], utensils[i, x]) <= 0.09f)
                        {
                            score -= 1;
                            if (deducted == false)
                            {
                                deducted = true;
                                PositionMarks++;
                            }
                        }

                        //Trying to find the max rotation among the objects
                        if (utensils[i, x].transform.rotation.y > maxUtensilRotation)
                        {
                            maxUtensilRotation = utensils[i, x].transform.rotation.y;

                        }
                        totalUtensilRotation += utensils[i, x].transform.rotation.y;
                    }
                    else
                    {
                        score -= 1;
                    }
                }
            }
        }

        float averageRot = maxUtensilRotation / utensils.Length;

        //The max rotation and the average rotation should not differ too much, if not counted as deducted score;
        if (maxUtensilRotation - averageRot >= 3f)
        {
            score -= 4;
        }
    }

    void SecondCheck()
    {
        int ScoretoDeduct = 0;
        bool deducted = false;
        Vector3 forkPos;
        Vector3 knifePos;

        for (int i = 0; i < 4; i++)
        {
            for (int x = 0; x < 3; x++)
            {
                //For each side of the table and each utensil
                utensils[i, x] = Areas[i].GetComponent<TableAreaCheck>().utensils[x];

                try
                {
                    //Find the correct utensil if it's there if not, score straight up -4 since objects are not even there
                    if (utensils[i, x].transform.Find("UtensilType").tag == "DinnerKnife")
                    {
                        dinnerKnife = utensils[i, x];
                    }
                    else if (utensils[i, x].transform.Find("UtensilType").tag == "DinnerFork")
                    {
                        dinnerFork = utensils[i, x];
                    }
                    else
                    {
                        score -= 1;
                    }
                }
                catch
                {
                    score -= 4;
                    if (deducted == false)
                    {
                        deducted = true;
                        PositionMarks++;
                    }
                    return;
                }
            }

            forkPos = new Vector3(dinnerFork.transform.localPosition.x, 0f, 0f);
            knifePos = new Vector3(dinnerKnife.transform.localPosition.x, 0f, 0f);

            if (Vector3.Distance(forkPos, knifePos) < 0.25f)
            {
                ScoretoDeduct += 1;
            }
        }


        score -= ScoretoDeduct;
    }

    void ThirdCheck()
    {
        int scoreToDeduct = 0;
        bool TopBottomClear = true; //the default is true to only be set false later if collision is detected
        bool deducted = false, deducted2 = false;

        Vector3 sidePlatePos;
        Vector3 butterKnifePos;
        GameObject knifeTopObject;
        GameObject knifeBottomObject;

        Collider[] knifeTop;
        Collider[] KnifeBottom;

        for (int i = 0; i < 4; i++)
        {

            //Basically find middle of the sideplate and the knife and top/btm
            for (int x = 0; x < 3; x++)
            {
                //Get the objects from the array in the tableareacheck script.
                utensils[i, x] = Areas[i].GetComponent<TableAreaCheck>().utensils[x];
                sidePlate = Areas[i].GetComponent<TableAreaCheck>().otherObjects[0];

                try
                {
                    if (utensils[i, x].transform.Find("UtensilType").tag == "ButterKnife")
                    {
                        butterKnife = utensils[i, x].transform.Find("Wall").gameObject;
                        knifeTopObject = utensils[i, x].transform.Find("Top wall").gameObject;
                        knifeBottomObject = utensils[i, x].transform.Find("Bottomwall").gameObject;

                        knifeTop = Physics.OverlapBox(knifeTopObject.transform.localPosition, new Vector3(0.01f, 0.01f, 0.01f), Quaternion.identity);
                        KnifeBottom = Physics.OverlapBox(knifeBottomObject.transform.localPosition, new Vector3(0.01f, 0.01f, 0.01f), Quaternion.identity);

                        foreach (Collider c in knifeTop)
                        {
                            if (c.gameObject.tag == "SidePlate")
                            {
                                TopBottomClear = false;
                            }
                        }

                        foreach (Collider c in KnifeBottom)
                        {
                            if (c.gameObject.tag == "SidePlate")
                            {
                                TopBottomClear = false;
                            }

                        }
                    }
                }
                catch
                {
                    score -= 1;
                    if (deducted == false)
                    {
                        deducted = true;
                        PositionMarks++;
                    }
                }
            }

            try
            {
                sidePlatePos = new Vector3(sidePlate.transform.localPosition.x, 0f, 0f);
                butterKnifePos = new Vector3(butterKnife.transform.localPosition.x, 0f, 0f);

                if (Vector3.Distance(sidePlatePos, butterKnifePos) < 0.023f && Vector3.Distance(sidePlatePos, butterKnifePos) > 0.033f)
                {
                    scoreToDeduct += 1;

                    if (TopBottomClear == false)
                    {
                        scoreToDeduct += 1;
                    }
                }
            }
            catch
            {
                score -= 1; //Probably no plate or knife
                if (deducted2 == false)
                {
                    deducted2 = true;
                    PositionMarks++;
                }
            }
        }

        score -= scoreToDeduct;
    }

    void FourthCheck()
    {
        tableMiddle = table.transform.Find("Middle").gameObject;
        Collider[] wineglassCollider;
        GameObject[] WineGlasses = new GameObject[4];

        int counter = 0;
        int counter2 = 0;
        bool deducted = false;

        Collider[] dinnerKnifeCollider = new Collider[4];
        GameObject[] dinnerKnife = new GameObject[4];

        //Find the wineglasses using collider check
        wineglassCollider = Physics.OverlapBox(tableMiddle.transform.position, new Vector3(0.8f, 0.8f, 0.8f), Quaternion.identity);
        foreach (Collider c in wineglassCollider)
        {
            if (c.gameObject.tag == "WineGlass")
            {
                WineGlasses[counter] = c.gameObject;
                counter++;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            Collider[] temp;
            if (WineGlasses[i] != null)
            {
                temp = Physics.OverlapBox(WineGlasses[i].transform.position, new Vector3(0.15f, 0.15f, 0.15f), Quaternion.identity);

                //After finding the wineglasses, find the dinnerknife closest to the wineglass by checking for colliders around it
                foreach (Collider c in temp)
                {
                    if (c != null)
                    {
                        if (c.gameObject.tag == "Utensils" && c.gameObject.transform.Find("UtensilType").tag == "DinnerKnife")
                        {
                            dinnerKnife[i] = c.gameObject;
                            break;
                        }
                    }
                }
            }
        }

        foreach (GameObject go in WineGlasses)
        {
            if (go != null)
            {
                if (go.transform.Find("StemBottom").gameObject != null)
                {
                    GameObject stem = go.transform.Find("StemBottom").gameObject;
                    if (dinnerKnife[counter2])
                    {
                        GameObject knifeTip = dinnerKnife[counter2].transform.Find("Glasswall").gameObject;
                        counter2++;

                        //>0,85 <1.25

                        if (Vector3.Angle(knifeTip.transform.position, stem.transform.position) < 0.85f || Vector3.Angle(knifeTip.transform.position, stem.transform.position) > 1.25f)
                        {
                            score -= 1;
                            if (deducted == false)
                            {
                                deducted = true;
                                PositionMarks++;
                            }
                        }

                        if (Vector3.Distance(knifeTip.transform.position, stem.transform.position) <= 0.1f)
                        {
                            score -= 1;
                            if (deducted == false)
                            {
                                deducted = true;
                                PositionMarks++;
                            }
                        }

                        PositionMarks -= gameObject.GetComponent<ChairCheck>().CheckChair();
                    }
                    else
                        score -= 2;
                }
            }
        }
    }

    void FifthCheck()
    {
        //Find the 4 wineglasses on the table first
        tableMiddle = table.transform.Find("Middle").gameObject;
        Collider[] wineglassCollider;
        GameObject[] WineGlasses = new GameObject[4];

        int counter = 0;
        bool deducted = false;

        wineglassCollider = Physics.OverlapBox(tableMiddle.transform.position, new Vector3(0.8f, 0.8f, 0.8f), Quaternion.identity);
        foreach (Collider c in wineglassCollider)
        {
            if (counter >= 4)
            {
                //Skip
            }
            else if (c.gameObject.tag == "WineGlass")
            {
                WineGlasses[counter] = c.gameObject;
                counter++;
            }
        }

        float[] distances = new float[4];
        float[] angles = new float[4];

        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                if (WineGlasses[i] != null && WineGlasses[i - 3] != null)
                {
                    distances[i] = Vector3.Distance(WineGlasses[i].transform.position, WineGlasses[i - 3].transform.position);
                    angles[i] = Vector3.Angle(WineGlasses[i].transform.position, WineGlasses[i - 3].transform.position);
                }
            }
            else if (WineGlasses[i] != null && WineGlasses[i + 1] != null)
            {
                distances[i] = Vector3.Distance(WineGlasses[i].transform.position, WineGlasses[i + 1].transform.position);
                angles[i] = Vector3.Angle(WineGlasses[i].transform.position, WineGlasses[i + 1].transform.position);
            }
            else
            {
                score -= 1;
                if (deducted == false)
                {
                    deducted = true;
                    PositionMarks++;
                }
                //Since user probably messed up and didn't place the wineglass remotely close to where it should be
            }
            //>3.3 <3.9 is good

            if (angles[i] < 3.0f || angles[i] > 4.0f || distances[i] > 4.0f || distances[i] < 3.0f)
            {
                score -= 1;
                if (deducted == false)
                {
                    deducted = true;
                    PositionMarks++;
                }
            }
        }
    }

    void SixthCheck()
    {
        //Check position of salt pepper mill to bar
        tableMiddle = table.transform.Find("Middle").gameObject;
        Collider[] tempCollider;
        GameObject[] SnP = new GameObject[4];
        bool deducted = false;

        tempCollider = Physics.OverlapBox(tableMiddle.transform.position, new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity);
        foreach (Collider c in tempCollider)
        {
            if (c.gameObject.tag == "Mills")
            {
                if (c.gameObject.transform.parent.transform.Find("UtensilType").tag == "SaltMill")
                {
                    SnP[0] = c.gameObject;
                }
                else if (c.gameObject.transform.parent.transform.Find("UtensilType").tag == "PepperMill")
                {
                    SnP[1] = c.gameObject;
                }
            }
        }

        if (SnP[0] != null && SnP[1] != null)
        {
            float SalttoTable, PeppertoTable, distanceBetween, angleBetween;
            GameObject bar = GameObject.Find("bar");
            SalttoTable = Vector3.Distance(SnP[0].transform.position, bar.transform.position);
            PeppertoTable = Vector3.Distance(SnP[1].transform.position, bar.transform.position);
            distanceBetween = Vector3.Distance(SnP[0].transform.position, SnP[1].transform.position);
            angleBetween = Vector3.Angle(SnP[0].transform.position, SnP[1].transform.position);

            if (SalttoTable > PeppertoTable)
            {
                score -= 1;
                if (angleBetween > 1.65f || angleBetween < 0.6f)
                {
                    score -= 1;
                    PositionMarks++;
                }
                if (distanceBetween < 0.05f || distanceBetween > 1.2f)
                {
                    score -= 1;
                    PositionMarks++;
                }
            }
        }

        //If it doesn't even exist then just deduct score
        else
        {
            score -= 4;
            if (deducted == false)
            {
                deducted = true;
                PositionMarks++;
            }
        }

        if (score < 0)
        {
            score = 0;
        }
        //As long as compare position of table, salt is closer to table than pepper, then check for angle and distance of s and p. if any out of bounds then deduct score;
        // can check if p and s is on the other half of the table
    }

    //Takes in the "Wall" Object which is the edge of the table & the item it's comparing distance with
    float TableFunc(GameObject wallNumber, GameObject itemNumber)
    {
        float properDistance = 0.0f;

        //Should take in the the "Wall 1" and the item's Bottomwall child
        //Bottom wall is just the bottom part of the object, comparing to the edge of the table for distance
        if (wallNumber.name == "Wall 1" || wallNumber.name == "Wall 2")
        {
            Vector3 wallVec = new Vector3(0f, 0f, wallNumber.transform.position.z);
            if (itemNumber.transform.Find("Bottomwall") != null)
            {
                Vector3 itemVec = new Vector3(0f, 0f, itemNumber.transform.Find("Bottomwall").transform.position.z);
                properDistance = Vector3.Distance(wallVec, itemVec);
                return properDistance;
            }
        }
        else
        {
            Vector3 wallVec = new Vector3(wallNumber.transform.position.x, 0f, 0f);
            if (itemNumber.transform.Find("Bottomwall") != null)
            {
                Vector3 itemVec = new Vector3(itemNumber.transform.Find("Bottomwall").transform.position.x, 0f, 0f);
                properDistance = Vector3.Distance(wallVec, itemVec);
                return properDistance;
            }
        }
        return properDistance;
    }

    public void Deductscore(int value)
    {
        //Used for when penalty
        score -= value;
    }

    public int getPenalty()
    {
        int actualScore = 8 - penaltyCount;

        if (actualScore < 0)
        {
            actualScore = 0;
        }
        return actualScore;
    }

    public int getPositioning()
    {
        int actualScore = 12 - PositionMarks;

        if (actualScore < 0)
        {
            actualScore = 0;
        }
        return actualScore;
    }
}

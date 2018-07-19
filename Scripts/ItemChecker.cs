using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    public static ItemChecker instance = null;
    public int ObjectiveNumber = 0, subObjectiveNumber = 1, reset = 0;
    public GameObject[] objectiveText = new GameObject[11];
    float timer = 0.0f;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance ==this)
        {
            Destroy(this.gameObject);
        }
    }

    //For objective 1
    bool item1 = false, item2 = false, item3 = false;

    void Start()
    {
        //set A - 3 Utensils
        objectiveText[0].SetActive(true);
    }

    void Update()
    {
        if (subObjectiveNumber == 1) //Prepare utensils
        {
            
            //Remove A - 3 Utensils
            objectiveText[0].SetActive(true);
        }

        else if (subObjectiveNumber == 2) //Napkin
        {
            objectiveText[0].SetActive(false);
            //Remove B - Place Napkin
            objectiveText[1].SetActive(true);
        }

        else if (subObjectiveNumber == 3)
        {
            objectiveText[1].SetActive(false);
            objectiveText[2].SetActive(true);
        }

        else if (subObjectiveNumber == 4)
        {
            objectiveText[2].SetActive(false);
            objectiveText[3].SetActive(true);
        }

        else if (subObjectiveNumber == 5)
        {
            objectiveText[3].SetActive(false);
            objectiveText[4].SetActive(true);
        }

        else if (subObjectiveNumber == 6)
        {
            objectiveText[4].SetActive(false);
            objectiveText[5].SetActive(true);
        }

        if (ObjectiveNumber == 1) //Fine Tuning, show the point form text
        {
            //Set 1 - All the fine tuning
            objectiveText[6].SetActive(true);
        }

        else if (ObjectiveNumber == 2)
        {
            //Set 2 - Pick up other items
            objectiveText[6].SetActive(false);
            objectiveText[7].SetActive(true);
        }
        else if (ObjectiveNumber == 3)
        {
            //Set 3 - Place Wineglass
            objectiveText[7].SetActive(false);
            objectiveText[8].SetActive(true);
        }
        else if(ObjectiveNumber==4)
        {
            objectiveText[8].SetActive(false);
            objectiveText[9].SetActive(true);
        }
        else if(ObjectiveNumber ==5)
        {
            objectiveText[9].SetActive(false);
            objectiveText[10].SetActive(true);
        }
        else if(ObjectiveNumber>5)
        {
            objectiveText[10].SetActive(false);
        }
    }

    public void GoNext()
    {
        if(subObjectiveNumber<6)
        {
            if(reset !=4)
            {
                objectiveText[subObjectiveNumber - 1].SetActive(false);
                subObjectiveNumber++;
            }
        }
        else if(subObjectiveNumber == 6)
        {
            if (reset < 5)
            {
                objectiveText[5].SetActive(false);
                subObjectiveNumber = 1;
                reset++;
            }
            if(reset ==4)
            {
                objectiveText[5].SetActive(false);
                objectiveText[6].SetActive(true);
                reset = 4;
                subObjectiveNumber = 7;
                ObjectiveNumber = 1;
            }
        }

        else if(ObjectiveNumber<5)
        {
            objectiveText[ObjectiveNumber + 5].SetActive(false);
            ObjectiveNumber++;
        }
    }

    public void GoBack()
    {
        if(reset<4)
        {
            if(subObjectiveNumber>1)
            {
                objectiveText[subObjectiveNumber - 1].SetActive(false);
                subObjectiveNumber--;
            }
        }
        else if(reset>=4)
        {
            if(ObjectiveNumber > 2)
            {
                objectiveText[ObjectiveNumber + 5].SetActive(false);
                ObjectiveNumber--;
            }
        }
    }
}

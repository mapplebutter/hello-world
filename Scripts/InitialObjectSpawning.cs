using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialObjectSpawning : MonoBehaviour
{
    public static InitialObjectSpawning instance = null;
    public GameObject[] TableSpawns, RackSpawns, CutlerySpawns, GlassSpawns, PlatesSpawns, sidePlateSpawns, cutleryPrefabs, NapkinSpawns;
    public GameObject table, rack, wineglass, plate, sideplate, saltmill, peppermill, chopstickRack, napkin, tray, traySpawn,cloth,clothOnPlate;

    /*Instructions
     Set table spawns = 4, drag the spawn empty gameobjects in
     Set cutlery prefabs
      
     ->AsianCeramicSpoon, ButterKnife Edited, DinnerFork Edited
     DinnerKnife Edited, AppFork, AppKnife,
     Chopstick, chopstickrest, DessertSpoon
     DinnerSpoon, SoupSpoon, SteakKnife
     
     Set object prefabs
     */

    /*
     Object - Tag
     Table - Table
     >Wall # - WallTable
     >Middle
     >>Wall # Area - WallTableArea ( 1 facing Z ), WallTableAreaX
     
     >Utensils as Utensils
     >>UtensilType - DinnerFork/DinnerKnife/ButterKnife/SidePlate
      
     >DinnerFork has "Wall" on right & Bottomwall
     >ButterKnife has "Topwall", "Bottomwall", "Wall"
     >DinnerKnife has "Glasswall" on top, "Bottomwall"
     */

    public List<GameObject> spawnObjects = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        //Spawn all the objects needed//
        RackSpawns = new GameObject[3];
        CutlerySpawns = new GameObject[12];
        GlassSpawns = new GameObject[7];
        PlatesSpawns = new GameObject[1];
        sidePlateSpawns = new GameObject[4];
        NapkinSpawns = new GameObject[4];
        SpawnTables();

        foreach (GameObject x in spawnObjects)
        {
            if (x.GetComponent<ObjectSound>())
            {
                x.GetComponent<ObjectSound>().enableSound = false;
            }
        }
    }

    private void Start()
    {
        InitialObjectSpawning.instance.Initialize();
    }

    public void EndGame()
    {
        GameManager.instance.RunEndGame();
    }

    void SpawnTables()
    {
        int TableAreaNum = Random.Range(1, 5), cutleryCounter = 0, usedCutleryCounter = 0;
        int[] usedCutlery = new int[12] { 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13 };
        int tempCutleryNum = Random.Range(0, 11);
        int usedCutleryIndex = 0;

        while (usedCutleryIndex < usedCutlery.Length - 1)
        {
            for (int t = 0; t < usedCutlery.Length;) //search if the number is unique
            {
                if (tempCutleryNum != usedCutlery[t])
                {
                    t++;
                }
                else
                {
                    t = 0;
                    tempCutleryNum = Random.Range(0, 11);
                }
            }
            usedCutlery[usedCutleryIndex] = tempCutleryNum;
            usedCutleryIndex++;
        }
        bool rotateOrNot = false;
        GameObject temp;

        //spawn table
        temp = Instantiate(table, TableSpawns[TableAreaNum - 1].transform.position, Quaternion.identity);

        //
        spawnObjects.Add(temp);
        //

        if (TableAreaNum == 1)
        {
            rotateOrNot = true;
            temp.transform.Rotate(0f, 90f, 0f);
        }
        else if (TableAreaNum == 2)
        {
            temp.transform.Rotate(0f, 180f, 0f);
        }
        else if (TableAreaNum == 3)
        {
            rotateOrNot = true;
            temp.transform.Rotate(0f, -90f, 0f);
        }

        traySpawn = TableSpawns[TableAreaNum - 1].transform.Find("TraySpawn").gameObject;
        temp = Instantiate(tray, traySpawn.transform.position, Quaternion.identity);

        //
        spawnObjects.Add(temp);
        //

        //The code below is the old spawn red cloth on the tray. Since the snap is applied to the plate, it would be better if the cloth was on the plate itself.
        //Vector3 clothSpawn = new Vector3(traySpawn.transform.position.x, traySpawn.transform.position.y + 0.2f, traySpawn.transform.position.z);
        //temp = Instantiate(cloth, clothSpawn, Quaternion.identity);

        //Spawn the Red Cloth on top of the Plate on the Cutlery table.
        clothOnPlate = TableSpawns[TableAreaNum - 1].transform.Find("Plates").transform.Find("PlateSpawn 1").gameObject;

        //Setting the red cloth to spawn above the plate rather than the serving tray.
        Vector3 clothSpawn = new Vector3(clothOnPlate.transform.position.x, clothOnPlate.transform.position.y + 0.2f, clothOnPlate.transform.position.z);
        temp = Instantiate(cloth, clothSpawn, Quaternion.identity);

        //
        spawnObjects.Add(temp);
        //


        //Find rack and cutlery location and spawns them.
        for (int i = 0; i < 3; i++) //for each tray
        {
            RackSpawns[i] = TableSpawns[TableAreaNum - 1].transform.Find("Rack " + (i + 1).ToString()).gameObject;
            temp = Instantiate(rack, RackSpawns[i].transform.position, Quaternion.identity);

            //
            spawnObjects.Add(temp);
            //


            if (rotateOrNot == false)
            {
                temp.transform.Rotate(0f, 90f, 0f);
            }

            for (int o = 0; o < 4; o++) // for each slot in the tray
            {
                CutlerySpawns[cutleryCounter] = RackSpawns[i].transform.Find("CutlerySpawns " + (o + 1).ToString()).gameObject;

                float gap = 0.2f;

                for (int u = 0; u < 4; u++)
                {
                    Vector3 newPosition = new Vector3(CutlerySpawns[cutleryCounter].transform.position.x, CutlerySpawns[cutleryCounter].transform.position.y + gap, CutlerySpawns[cutleryCounter].transform.position.z);

                    temp = Instantiate(cutleryPrefabs[usedCutleryCounter], newPosition, Quaternion.identity);

                    //
                    spawnObjects.Add(temp);
                    //


                    if (rotateOrNot == false)
                    {
                        temp.transform.Rotate(0f, 90f, 0f);
                    }
                    gap += 0.1f;
                }
                cutleryCounter++;
                usedCutleryCounter++;
            }
        }

        //Find glass and mill random spawn location and spawns them
        int NumberOfGlass = 0;


        for (int p = 0; p < 7; p++)
        {
            GlassSpawns[p] = TableSpawns[TableAreaNum - 1].transform.Find("Glass&Mills").transform.Find("GlassSpawn " + (p + 1).ToString()).gameObject;

            if (NumberOfGlass < 4)
            {
                temp = Instantiate(wineglass, GlassSpawns[p].transform.position, Quaternion.identity);

                //
                spawnObjects.Add(temp);
                //


                NumberOfGlass++;
            }
            else if (p == 5)
            {
                temp = Instantiate(peppermill, GlassSpawns[p - 1].transform.position, Quaternion.identity);

                //
                spawnObjects.Add(temp);
                //

                temp = Instantiate(saltmill, GlassSpawns[p].transform.position, Quaternion.identity);

                //
                spawnObjects.Add(temp);
                //

            }

        }

        //Find plate,sideplate and cloth spawn location to randomly spawn them
        for (int j = 0; j < 4; j++)
        {
            sidePlateSpawns[j] = TableSpawns[TableAreaNum - 1].transform.Find("Plates").transform.Find("SidePlateSpawn " + (j + 1).ToString()).gameObject;
            NapkinSpawns[j] = TableSpawns[TableAreaNum - 1].transform.Find("Napkins").transform.Find("NapkinSpawn " + (j + 1).ToString()).gameObject;

            //temp = Instantiate(plate, PlatesSpawns[j].transform.position, Quaternion.identity);
            temp = Instantiate(sideplate, sidePlateSpawns[j].transform.position, Quaternion.identity);

            //
            spawnObjects.Add(temp);
            //

            temp = Instantiate(napkin, NapkinSpawns[j].transform.position, Quaternion.identity);

            //
            spawnObjects.Add(temp);
            //

        }

        //spawns one plate instead of multiple plates
        PlatesSpawns[0] = TableSpawns[TableAreaNum - 1].transform.Find("Plates").transform.Find("PlateSpawn 1").gameObject;
        temp = Instantiate(plate, PlatesSpawns[0].transform.position, Quaternion.identity);

        //
        spawnObjects.Add(temp);
        //

    }

    GameObject[] findObjects(string type)
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag(type);
        return temp;
    }
}
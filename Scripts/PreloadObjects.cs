using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadObjects : MonoBehaviour {

    /// <summary>
    /// This script preloads objects that will be activated after certain events occur (breaking, etc.)
    /// </summary>
    public static PreloadObjects instance = null;
    public int brokenGlassCount = 4;
    public int brokenDinnerPlateCount = 1;
    public int brokenSideplateCount = 4;

    public GameObject brokenGlassPrefab, brokenDinnerPlatePrefab, brokenSideplatePrefab;

    protected List<GameObject> brokenGlassObjects, brokenDinnerPlateObjects, brokenSideplateObjects;

	// Use this for initialization
	void Start () {
        instance = this;
        brokenGlassObjects = new List<GameObject>();
        brokenDinnerPlateObjects = new List<GameObject>();
        brokenSideplateObjects = new List<GameObject>();
        
        PopulateBrokenGlass();
        PopulateBrokenDinnerPlates();
        PopulateBrokenSideplates();
	}

    protected void PopulateBrokenGlass()
    {
        for (int i = 0; i < brokenGlassCount; i++)
        {
            GameObject temp = Instantiate(brokenGlassPrefab) as GameObject;
            brokenGlassObjects.Add(temp);
            temp.SetActive(false);
        }
    }

    protected void PopulateBrokenDinnerPlates()
    {
        for (int i = 0; i < brokenDinnerPlateCount; i++)
        {
            GameObject temp = Instantiate(brokenDinnerPlatePrefab) as GameObject;
            brokenDinnerPlateObjects.Add(temp);
            temp.SetActive(false);
        }
    }

    protected void PopulateBrokenSideplates()
    {
        for (int i = 0; i < brokenSideplateCount; i++)
        {
            GameObject temp = Instantiate(brokenSideplatePrefab) as GameObject;
            brokenSideplateObjects.Add(temp);
            temp.SetActive(false);
        }
    }

    public GameObject GetPooledObj(ObjectBreaking.TablewareType type)
    {
        GameObject ret = null;
        switch(type)
        {
            case ObjectBreaking.TablewareType.DinnerPlate:
                ret = GetDinnerPlate();
                break;
            case ObjectBreaking.TablewareType.Glass:
                ret = GetGlass();
                break;
            case ObjectBreaking.TablewareType.Sideplate:
                ret = GetSideplate();
                break;
        }

        return ret;
    }

    private GameObject GetDinnerPlate()
    {
        foreach (GameObject obj in brokenDinnerPlateObjects)
        {
            if(!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    private GameObject GetGlass()
    {
        foreach (GameObject obj in brokenGlassObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    private GameObject GetSideplate()
    {
        foreach (GameObject obj in brokenSideplateObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }


}

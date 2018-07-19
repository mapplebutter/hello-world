using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePropEnable : MonoBehaviour {

    public string propName = "";
    bool appear = true;
	
	// Update is called once per frame
	void Update () {
        if (appear) GetComponent<MeshRenderer>().enabled = true;
        else GetComponent<MeshRenderer>().enabled = false;
	}


    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        while (go.transform.parent != null) go = go.transform.parent.gameObject;


        if (go.name == propName || go.name == propName + "(Clone)") appear = false;
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        while (go.transform.parent != null) go = go.transform.parent.gameObject;

        if (go.name == propName || go.name == propName + "(Clone)") appear = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePropGuided : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (TitleMenu.instance.gameMode == TitleMenu.Mode.Guided) gameObject.SetActive(true);
        else gameObject.SetActive(false);
	}
}

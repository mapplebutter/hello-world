using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAudioAdjust : MonoBehaviour {

    public GameObject sliderhandler;
    public GameObject speaker;
    public Slider slider;
    float sliderValue = 0.5f;

    AudioSource[] audioSources = new AudioSource[4];

	// Use this for initialization
	void Start () {
		for (int i =0; i < 4; i++)
        {
            audioSources[i] = speaker.transform.GetChild(i).GetComponent<AudioSource>();
            audioSources[i].Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (sliderhandler.GetComponent<OnHover>().isTrue)
            sliderValue = slider.value;
        else slider.value = sliderValue;
	}

    public void ChangeGameBGM(float value)
    {
        if (sliderhandler.GetComponent<OnHover>().isTrue)
            foreach (AudioSource x in audioSources) x.volume = value;
    }
}

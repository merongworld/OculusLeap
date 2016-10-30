using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public class WorldMGR : MonoBehaviour {

    private Camera cam;
    private Slider slider;
    [Header("Config")]
    public GameObject OculusCamObj; 

	// Use this for initialization
	void Start () {
        cam = OculusCamObj.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackgroundColorChange()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        cam.backgroundColor = new Color(slider.value, slider.value, slider.value, 1);
    }
}

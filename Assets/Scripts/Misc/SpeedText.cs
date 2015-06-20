using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class SpeedText : MonoBehaviour {

    Text text;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Speed x" + Mathf.Floor(Time.timeScale);
	}
}

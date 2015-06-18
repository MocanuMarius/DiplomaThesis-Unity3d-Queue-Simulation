using UnityEngine;
using System.Collections;

public class ScrollBarTime : MonoBehaviour {
	// Use this for initialization

	void Start () {
		Time.timeScale = 5;
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void changeTime(float newValue){
		Time.timeScale = newValue;
	}
}

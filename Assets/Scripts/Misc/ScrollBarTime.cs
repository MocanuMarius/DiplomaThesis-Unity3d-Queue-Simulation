using UnityEngine;
using System.Collections;

public class ScrollBarTime : MonoBehaviour {
	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void changeTime(float newValue){
        if (StateManager.simState == StateManager.SimState.Finished)
            Time.timeScale = 0;
        else
		    Time.timeScale = newValue;
	}
}

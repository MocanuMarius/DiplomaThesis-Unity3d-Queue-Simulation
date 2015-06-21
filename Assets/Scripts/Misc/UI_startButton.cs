using UnityEngine;
using System.Collections;

public class UI_startButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void start()
    {
        StateManager.simState = StateManager.SimState.Running;
        GameObject.FindGameObjectWithTag("canvas_mainMenu").GetComponent<Canvas>().enabled = false;
        GameObject.FindGameObjectWithTag("canvas_overlayMenu").GetComponent<Canvas>().enabled = true;
    }
}

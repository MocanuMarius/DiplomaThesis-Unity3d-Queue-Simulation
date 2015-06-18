using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
	public static int maxNumber;
	public int timeHorizonMinutes =5;
	public int timeScale = 1;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timeHorizonMinutes < Time.timeSinceLevelLoad/60) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}

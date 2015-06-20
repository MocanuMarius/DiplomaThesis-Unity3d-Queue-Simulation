using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_timeSelect : MonoBehaviour {

    int minutes;
	// Use this for initialization
	void Awake () {
      
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Text>().text != "")
        {
            int.TryParse(this.GetComponent<Text>().text,out minutes);
            StateManager.Instance().timeHorizonSeconds = minutes*60;
        }
	}
}

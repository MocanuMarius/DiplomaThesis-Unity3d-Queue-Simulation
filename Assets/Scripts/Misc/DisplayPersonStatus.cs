using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPersonStatus : MonoBehaviour {

    Text text;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Total:" + PersonManager.Instance().numberOfPersonsTotal +
            "  | Inside:" + PersonManager.Instance().persons.Count +
            "  | Aborted:" + PersonManager.Instance().numberOfPersonsAborted +
            "  | Lost:" + PersonManager.Instance().numberOfPersonsNotSpawned +
            "  | Completed:" + Mathf.Floor((float)StateManager.Instance().getPercentElapsed()*100) + "%";
	}
}

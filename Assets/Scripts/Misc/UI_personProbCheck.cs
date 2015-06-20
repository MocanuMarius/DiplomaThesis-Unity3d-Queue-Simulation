using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_personProbCheck : MonoBehaviour {

    int interval;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void setProbability(string value)
    {
        int interval;
        if (value != "")
        {
            int.TryParse(value, out interval);
            if (interval > 0)
                PersonManager.spawnTimeInterval = interval;
        }
    }
}

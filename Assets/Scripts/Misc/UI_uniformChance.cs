using UnityEngine;
using System.Collections;

public class UI_uniformChance : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setUniformChance(string value)
    {
        int interval;
        if (value != "")
        {
            int.TryParse(value, out interval);
            if (interval > 0 && interval <= 100)
                DistributionManager.uniformChance = interval;
        }
    }

}

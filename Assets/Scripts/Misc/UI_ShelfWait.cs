using UnityEngine;
using System.Collections;

public class UI_ShelfWait : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setShelfWaitTime(string value)
    {
        int shelfWait = 20;
        int.TryParse(value, out shelfWait);
        if (shelfWait > 0)
            PersonManager.Instance().shelfWaitingTime = shelfWait;
    }
}

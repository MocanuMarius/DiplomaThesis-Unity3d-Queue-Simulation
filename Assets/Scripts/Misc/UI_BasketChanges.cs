using UnityEngine;
using System.Collections;

public class UI_BasketChanges : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void updateMinItem(string value) {
        int minItem = 1;
        int.TryParse(value, out minItem);
        if (minItem > 0)
            PersonManager.Instance().minItems = minItem;
    }
    public void updateMaxItem(string value)
    {
        int maxItem = 10;
        int.TryParse(value, out maxItem);
        if (maxItem > 0)
            PersonManager.Instance().maxItems = maxItem;
    }
    public void updateMinItemWait(string value)
    {
        int minItemWait = 1;
        int.TryParse(value, out minItemWait);
        if (minItemWait > 0)
            PersonManager.Instance().minItemTime = minItemWait;
    }
    public void updateMaxItemWait(string value)
    {
        int maxItemWait = 10;
        int.TryParse(value, out maxItemWait);
        if (maxItemWait > 0)
            PersonManager.Instance().maxItemTime = maxItemWait;
    }
}

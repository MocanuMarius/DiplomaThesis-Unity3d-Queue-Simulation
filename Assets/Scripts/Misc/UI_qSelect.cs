using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_qSelect : MonoBehaviour {

	// Use this for initialization
    Slider qSlider;
    Text qText;
	void Start () {
        qSlider = GetComponentInChildren<Slider>();
        Text[] texts= GetComponentsInChildren<Text>();
        foreach (Text txt in texts)
        {
            if (txt.gameObject.name == "qValues")
                qText = txt;
        }
        StateManager.Instance().numberOfDisabledQueues = (int)qSlider.value;
   
	}
	
	// Update is called once per frame
	void Update () {
        StateManager.Instance().numberOfDisabledQueues = 5 - (int)qSlider.value;
        StateManager.disableQueues();
        qText.text = "" + (5 - StateManager.Instance().numberOfDisabledQueues);
	}
}

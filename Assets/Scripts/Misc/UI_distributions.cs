using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Misc;

public class UI_distributions : MonoBehaviour {
    public static ToggleGroup toggleGroup;
    private Toggle currentSelectedToggle;
    Toggle toggle;
	// Use this for initialization

	void Awake () {
       toggleGroup = GameObject.Find("distribPanel").GetComponent<ToggleGroup>();
	}
	
	// Update is called once per frame
	void Update () {
        if (StateManager.simState == StateManager.SimState.Menu)
        {
            IEnumerator<Toggle> toggleEnum = toggleGroup.ActiveToggles().GetEnumerator();
            toggleEnum.MoveNext();
            toggle = toggleEnum.Current;
            if (toggle.name == "normalDistribution")
            {
                DistributionManager.setNormal();
            }
            else if (toggle.name == "triangularDistribution")
            {
                DistributionManager.setTriangle();
            }
            else if (toggle.name == "erlangDistribution")
            {
                DistributionManager.setErlang();
            }
            else if (toggle.name == "equalUniform")
            {
                DistributionManager.setUniform();
            }
        }
	}
}


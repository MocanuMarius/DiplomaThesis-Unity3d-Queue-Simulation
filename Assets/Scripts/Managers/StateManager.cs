using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
    private static StateManager stateManager;
	public static int maxNumber;
	public double timeHorizonSeconds = 600;
    private double timeElapsedSeconds= 0;
	public int timeScale = 0;
    public int numberOfDisabledQueues = 0;
    public enum SimState { Menu,Finished,Running,GatheringData };
    public static SimState simState;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
	}
    void Awake()
    {
        simState = SimState.Menu;
    }
	
	// Update is called once per frame
	void Update () {
        timeElapsedSeconds = Time.timeSinceLevelLoad;
        if (timeElapsedSeconds > timeHorizonSeconds)
        {
            //SHOW STATISTICS AND GUI
            Time.timeScale = 0;
        }
	}
    public double getPercentElapsed()
    {
        if (timeElapsedSeconds / timeHorizonSeconds < 1)
            return timeElapsedSeconds / timeHorizonSeconds;
        else return 1;
    }
    void DisablePersonAndLight(Q q)
    {
       Transform[] personsTrans = q.GetComponentsInChildren<Transform>();
       foreach (Transform trans in personsTrans)
       {
           if (trans.gameObject.tag == "qPerson")
           {
               trans.gameObject.SetActive(false);
           }
           if (trans.gameObject.tag == "qLight")
           {
               trans.gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);
           }
           if (trans.gameObject.tag == "qGate")
           {
               trans.gameObject.GetComponent<MeshRenderer>().enabled = true;
           }
       }

    }
    public static StateManager Instance()
    {
        if (!stateManager)
        {
            stateManager = FindObjectOfType(typeof(StateManager)) as StateManager;
        }
        return stateManager;
    }
    public static void disableQueues(){
               for (int i = 0; i < StateManager.Instance().numberOfDisabledQueues; i++)
        {
            QManager.Instance().queues[i].isActivated = false;
            StateManager.Instance().DisablePersonAndLight(QManager.Instance().queues[i]);
        }
    }
}

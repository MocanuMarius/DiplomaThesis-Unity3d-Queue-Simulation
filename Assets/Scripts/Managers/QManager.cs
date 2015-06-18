using UnityEngine;
using System.Collections;

public class QManager : MonoBehaviour {
	private static QManager qManager;
	Q[] queues;
	// Use this for initialization
	void Start () {
		queues = new Q[5];
		GameObject[] _queues = GameObject.FindGameObjectsWithTag ("Q");
		for (int i=0; i<5; i++) {
			queues[i] = _queues[i].GetComponent<Q>();
		}
	}

	public bool allocatePersonToFittestQueue(Person person){
		bool allocated = false;
		int shortestQueue=-1;
		int minTime = int.MaxValue;
		for (int i=0; i<5; i++) {
			if (queues [i].getSlotsAvailable () > 0) {
				if (queues [i].getTotalWaitTime () < minTime) {
					shortestQueue = i;
					minTime = queues [i].getTotalWaitTime ();
				}
			}
		}
		if (shortestQueue != -1){
				allocated = true;
				queues[shortestQueue].putPersonInQ(person);
		}
		return allocated;
	}
	// Update is called once per frame
	void Update () {
	
	}
	public static QManager Instance (){
		if (!qManager) {
			qManager= FindObjectOfType(typeof (QManager)) as QManager;
		}
		return qManager;
	}
}

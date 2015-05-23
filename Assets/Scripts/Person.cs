using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	Renderer[] shoulders;
	NavMeshAgent agent;
	Transform thisTransform;
	enum BuyState { FinishedTotal,FinishedItem,Buying,Start };
	Vector3 currentDestination;
	Color darkGrey = new Color(0.1f,0.1f,0.1f);
	Color darkRed = new Color (0.8f, 0, 0);
	BuyState buyState;
	bool finishedCoroutineTimer=false;
	float finishedTime;
	// Use this for initialization
	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		thisTransform = GetComponent<Transform> ();
		Color[] colorSet = {Color.white,Color.black,darkRed,darkGrey};
		shoulders = GetComponentsInChildren<Renderer> ();
		Color color = colorSet [Random.Range (0, 4)];
		for (int i=1;i<3;i++){
			shoulders[i].material.color = color;
		}
	}
	void Start(){
		currentDestination = PersonManager.Instance ().getNextBuyPosition ();
		buyState = BuyState.Buying;
		agent.SetDestination ( currentDestination );
	}
	
	// Update is called once per frame
	void Update () {
		if (!agent.pathPending)
		if ( Mathf.Floor(agent.remainingDistance) < 1 && buyState == BuyState.Buying) {
			finishedTime = Time.timeSinceLevelLoad;
			buyState = BuyState.FinishedItem;
		}
		if (buyState == BuyState.FinishedItem) {
			if (finishedTime + 15.0f < Time.timeSinceLevelLoad){
				//StartCoroutine("setBiggerPriorityForSeconds");
				currentDestination = PersonManager.Instance().getNextBuyPosition();
				agent.SetDestination ( currentDestination);
				buyState = BuyState.Buying;
			}
		}
	}

	IEnumerable setBiggerPriorityForSeconds(){
		if (!finishedCoroutineTimer) {
			finishedCoroutineTimer=true;
			agent.avoidancePriority = 50;
			yield return new WaitForSeconds (1.5f);
		}
		else {
			finishedCoroutineTimer=false;
			agent.avoidancePriority = 50;
		}
	}
}

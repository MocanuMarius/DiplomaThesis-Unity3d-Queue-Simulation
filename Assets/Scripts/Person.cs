using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	Renderer[] shoulders;
	NavMeshAgent agent;
	Color shoulderColor;
	public Bucket bucket;
	Transform thisTransform;
	TextMesh thisText;
	public enum BuyState { FinishedTotal, FinishedItem, Buying, Start, Waiting,WaitingFirstInLine, ExitingIntermmediate1,ExitingIntermmediate2, Exiting };
	Vector3 currentDestination;
	public BuyState buyState;
	float finishedTime;
	public int totalTimeInQueue=0;
    public int insideTime = 0;
	public bool hasAbandoned;
	int totalTimeFirstInQ=0;
	// Use this for initialization
	void Awake () {
		hasAbandoned = false;
		bucket = PersonManager.Instance().getNewBucket ();
		agent = GetComponent<NavMeshAgent> ();
		thisTransform = GetComponent<Transform> ();
		thisTransform.SetParent (GameObject.Find ("People").transform);
		shoulders = GetComponentsInChildren<Renderer> ();
		shoulderColor = PersonColors.Instance().getNewColor();
		buyState = BuyState.Start;
		for (int i=1;i<3;i++){
			shoulders[i].material.color = shoulderColor;
		}
	}
	void Start(){
        StartCoroutine("startCountingInsideTime");
        if (QManager.Instance().isThereAnySlotsLeft() == false)
        {
             buyState = BuyState.ExitingIntermmediate1;
            agent.SetDestination(Locations.intermediateExitLocation1);
        }
        else
        {
            agent.autoRepath = true;
            currentDestination = PersonManager.Instance().getNextBuyPosition();
            buyState = BuyState.Buying;
            agent.SetDestination(currentDestination);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!agent.pathPending && buyState!= BuyState.FinishedTotal)
			if ( Mathf.Floor(agent.remainingDistance) < 1 && buyState == BuyState.Buying) {
				finishedTime = Time.timeSinceLevelLoad;
				buyState = BuyState.FinishedItem;
			}
		if (buyState == BuyState.FinishedItem) {
			if (finishedTime + PersonManager.Instance().shelfWaitingTime < Time.timeSinceLevelLoad){
				bucket.finishItem();
				if (bucket.isBucketFinished() == false)
				{
					currentDestination = PersonManager.Instance().getNextBuyPosition();
					agent.SetDestination ( currentDestination);
					buyState = BuyState.Buying;
				}
				else{
					//Code to go and be queueded
					buyState = BuyState.FinishedTotal;
					agent.SetDestination(getClosestQAllocationArea());
				}
			}
		}
		//DETECT IF PERSON REACHED QUEUE SPOT
		if (!agent.pathPending)
		if (Mathf.Floor (agent.remainingDistance) < 1 
			&& (buyState == BuyState.WaitingFirstInLine || buyState == BuyState.Waiting)) {
				if (this.totalTimeInQueue == 0)
					StartCoroutine("startCountingQWaitTime");
		}
		if (!agent.pathPending)
		if (Mathf.Floor (agent.remainingDistance) < 1 && buyState == BuyState.ExitingIntermmediate1) {

			agent.SetDestination(Locations.intermediateExitLocation2);
			buyState = BuyState.ExitingIntermmediate2;
		}
		if (!agent.pathPending)
		if (Mathf.Floor (agent.remainingDistance) < 1 && buyState == BuyState.ExitingIntermmediate2) {

			buyState = BuyState.Exiting;
			agent.SetDestination(Locations.exitLocation);
		}

		if (!agent.pathPending)
		if (Mathf.Floor (agent.remainingDistance) < 1 && buyState == BuyState.Exiting) {
			//DO some kind of statistics
            if (hasAbandoned == true)
            {
                PersonManager.Instance().numberOfPersonsAborted++;
            }
            PersonManager.Instance().persons.Remove(this.gameObject);
			Destroy(this.gameObject);
		}

	}
	public Vector3 getClosestQAllocationArea(){
		Collider qaRenderer = GameObject.FindGameObjectWithTag ("Allocation Area").GetComponent<Collider> ();
		return qaRenderer.ClosestPointOnBounds(agent.transform.localPosition);
	}
	public string getPersonInfo(){
		return "State:" + (bucket.ToString()) as string + "\n" +
			"BucketFinished:" + (this.bucket.isBucketFinished()) as string;
	}

	void OnTriggerEnter(Collider col){
		if (buyState == BuyState.FinishedTotal && col.transform.name == "Allocation Area") {
			bool allocated = QManager.Instance ().allocatePersonToFittestQueue (this.GetComponent<Person> ());
			if (!allocated) {
				buyState = BuyState.ExitingIntermmediate1;
				goToInterrmediateLocation();
			}
		}
	}
	void goToInterrmediateLocation(){
		agent.SetDestination (Locations.intermediateExitLocation1);
	}
	IEnumerator startCountingQWaitTime(){
		while (this.buyState == BuyState.Waiting 
		       || this.buyState == BuyState.WaitingFirstInLine){
					totalTimeInQueue+=1;
					if (this.buyState == BuyState.WaitingFirstInLine)
						totalTimeFirstInQ+=1;
					if (this.buyState == BuyState.WaitingFirstInLine && totalTimeFirstInQ > bucket.totalWaitingTime)
					{
						this.buyState = BuyState.ExitingIntermmediate2;
					}
					yield return new WaitForSeconds(1);
		}
	}
    IEnumerator startCountingInsideTime()
    {
        while (true)
        {
            insideTime++;
            yield return new WaitForSeconds(1);
        }
    }
}

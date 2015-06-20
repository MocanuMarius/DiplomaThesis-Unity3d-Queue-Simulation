using UnityEngine;
using System.Collections;

public class Q : MonoBehaviour {
	private Slot[] slots;
	public int numberOfPersons=0;
	public bool isActivated;
	public float speedToQslot = 0.5f;
	bool hasFirstPersonFinished;
	int timeSoFar;
	// Use this for initialization
	void Awake(){
		isActivated = true;
		slots = new Slot[10];
		Transform[] slotTransforms = gameObject.GetComponentsInChildren<Transform> ();
		foreach (Transform trans in slotTransforms){
			if (trans.tag == "qSlot"){
				slots[int.Parse(trans.gameObject.name)] = new Slot();
				slots[int.Parse(trans.gameObject.name)].position = trans.position;
			}
		}
	}
	void Start () {}
	void Update () {
		//If a person is registered in the queue but finished waiting and going to
		//the nearest exit position- only if he is the first in line
		if (slots[0].isOccupied && slots[0].person.buyState == Person.BuyState.Exiting) {
			pushQslots();
		}
	}
	public int getTotalWaitTime (){
		int totalTime = 0;
		for (int i=0;i<10;i++){
			if (slots[i].person !=null)
				totalTime+= slots[i].person.bucket.totalWaitingTime;
		}
		return totalTime;
	}
	public int getSlotsAvailable(){
		int nrSlots = 0;
		foreach (Slot slot in slots) {
			if (!slot.isOccupied)
				nrSlots++;
		}
		return nrSlots;
	}
	public void putPersonInQ (Person person){
	
		for (int i =0; i<10; i++) {
			if (!(slots[i].isOccupied)){
				slots[i].occupySlot(person);
				if (slots[i].person != person) putPersonInQ(person);
				NavMeshAgent agent = person.gameObject.GetComponent<NavMeshAgent>();
				agent.SetDestination(slots[i].position);
				if (i==0){
					person.buyState = Person.BuyState.WaitingFirstInLine;
				}
				else{
					person.buyState = Person.BuyState.Waiting;
				}
				break;
			}
		}
	}
	public void pushQslots(){
		slots [0].person = null;
		if (slots [1].isOccupied)
			slots [1].person.buyState = Person.BuyState.WaitingFirstInLine;
		int i = 1;
		while (i!=10 && slots[i].isOccupied) {
				slots [i - 1].person = slots [i].person;
				slots [i - 1].person.GetComponent<NavMeshAgent> ().SetDestination (slots [i - 1].position);	
				slots [i].person = null;
				i++;
			}
		}

}
public class Slot{
	public Person person;
	public Vector3 position;
	public bool isOccupied {
		get{return person != null;}
	}
	public void occupySlot(Person _person){
		person = _person;
	}
}






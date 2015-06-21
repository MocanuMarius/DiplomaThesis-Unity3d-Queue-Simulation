using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonManager : MonoBehaviour {
	// Use this for initialization
	//Put gameManager
	public Transform PersonPrefab;
	public int minItems;
	public int maxItems;
	public int minItemTime;
	public int maxItemTime;
	public int shelfWaitingTime;
	public static int spawnTimeInterval;

    public int numberOfPersonsTotal = 0;
    public int numberOfPersonsNotSpawned = 0;
    public int personsInsideLimit = 280;
    public int numberOfPersonsAborted = 0;

	private static PersonManager personManager;
	Renderer rowRenderer;
	public List<GameObject> persons = new List<GameObject>();
	GameObject[] rows;
	Bounds rowBounds;
	Vector3 randomizedPosition;
	int selectedRowNumber;

	void Start () {
        spawnTimeInterval = 1;
		StartCoroutine (InstantiateNewPerson ());
	}
	void Update () {
	}
	public Vector3 getNextBuyPosition(){
		rows = GameObject.FindGameObjectsWithTag ("rowTransforms");
		selectedRowNumber = Random.Range (0, rows.Length);
		rowRenderer = rows [selectedRowNumber].GetComponent<Renderer>();
		rowBounds = rowRenderer.bounds;
		randomizedPosition = new Vector3 (
			rowBounds.center.x + Random.Range (-rowBounds.extents.x, rowBounds.extents.x),
			0,
			rowBounds.center.z + Random.Range (-rowBounds.extents.z, rowBounds.extents.z)
			);
		return randomizedPosition;
	}
	public Bucket getNewBucket(){
		return new Bucket();
	}
	IEnumerator InstantiateNewPerson(){
		while (true) {
            if (DistributionManager.getDistrib())
            {
                numberOfPersonsTotal++;
                if (persons.Count < personsInsideLimit)
                {
                    GameObject newPerson = Instantiate(PersonPrefab, Locations.Instance().getNewSpawnLocation(), Quaternion.identity) as GameObject;
                    GameObject currentPerson = GameObject.Find("Person(Clone)");
                    persons.Add(currentPerson);
                    currentPerson.name = "Person " + (persons.Count) as string;
                }
                else numberOfPersonsNotSpawned++;
            }
		yield return new WaitForSeconds (spawnTimeInterval);
		}
	}
	public static PersonManager Instance (){
		if (!personManager) {
			personManager= FindObjectOfType(typeof (PersonManager)) as PersonManager;
		}
		return personManager;
	}
}
//BUCKET CLASS
public class Bucket {
	public List<Item> items;
	public int totalWaitingTime;
	public Bucket(){
		items = new List<Item>();
		int randomListOfItems = Random.Range (PersonManager.Instance ().minItems, PersonManager.Instance ().maxItems);
		for (int i=0; i<randomListOfItems; i++) {
			int randomTimeForItem = Random.Range(PersonManager.Instance().minItemTime,PersonManager.Instance().maxItemTime);
			items.Add(new Item(randomTimeForItem));
			totalWaitingTime+=randomTimeForItem;
		}
	}
	public bool isBucketFinished (){
		if (items.Count > 0)
			return false;
		else
			return true;
	}
	public void finishItem(){
		items.RemoveAt (items.Count-1);
	}

}

// THI IS ANOTHER CLASS
public class Item{
	private int waitingTime;
	private bool _bought =false;
	public Item(int waitingTime){
		this.waitingTime = waitingTime;
	}
	public void setWaitingTime(int waitingTime){
		this.waitingTime = waitingTime; 
	}
	public bool bought {
		get{return _bought;}
		set{
			_bought=value;
		}
	}

}
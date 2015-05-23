using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	//public Transform goal;
	NavMeshAgent agent;
	public float minSpeed;
	public float maxSpeed;
	float speed;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = Random.Range(minSpeed,maxSpeed);
		agent.acceleration = Random.Range (3,10);
	}
	
	// Update is called once per frame
	void Update () {
		Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayLocation = new RaycastHit ();
		if (Input.GetMouseButtonDown(0))
		{
			if(Physics.Raycast(mouseRay,out rayLocation))
			{
				Physics.Raycast(mouseRay,out rayLocation);
				agent.destination = rayLocation.point;
			}
		}	
	}
}

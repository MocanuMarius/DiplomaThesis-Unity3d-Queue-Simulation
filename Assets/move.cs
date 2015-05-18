using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	public Transform goal;
	// Use this for initialization
	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

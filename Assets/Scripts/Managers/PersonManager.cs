using UnityEngine;
using System.Collections;

public class PersonManager : MonoBehaviour {
	// Use this for initialization
	private static PersonManager personManager;
	Renderer rowRenderer;
	GameObject[] rows;
	Bounds rowBounds;
	Vector3 randomizedPosition;
	int selectedRowNumber;

	public static PersonManager Instance (){
		if (!personManager) {
			personManager= FindObjectOfType(typeof (PersonManager)) as PersonManager;
		}
		return personManager;
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
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

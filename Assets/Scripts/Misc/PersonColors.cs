using UnityEngine;
using System.Collections;

public class PersonColors : MonoBehaviour {
	static Color darkGrey = new Color(0.1f,0.1f,0.1f);
	static Color darkRed = new Color (0.8f, 0, 0);
	Color[] colorSet = {Color.white,Color.black,darkRed,darkGrey};
	private static PersonColors personColors;
	// Use this for initialization
	public static PersonColors Instance (){
		if (!personColors) {
			personColors= FindObjectOfType(typeof (PersonColors)) as PersonColors;
		}
		return personColors;
	}
	void Awake () {
	}
	public Color getNewColor(){
		Color color = colorSet [Random.Range (0, 4)];
		return color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

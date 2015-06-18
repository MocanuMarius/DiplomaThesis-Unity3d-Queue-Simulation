using UnityEngine;
using System.Collections;

public class Locations : MonoBehaviour {

	private static Locations locations;
	public static Vector3 exitLocation = new Vector3 (-25f, 0f, 010.45574f);
	public static Vector3 intermediateExitLocation1 = new Vector3 (-14.98f, 0, -9.27f);
	public static Vector3 intermediateExitLocation2 = new Vector3 (-25f, 0, -8.52f);
	public Vector3 getNewSpawnLocation(){
		return  new Vector3 (7.47f, -0.05f, Random.Range (1.1f, -3.7f));
	}
	public static Locations Instance (){
		if (!locations) {
			locations= FindObjectOfType(typeof (Locations)) as Locations;
		}
		return locations;
	}
}

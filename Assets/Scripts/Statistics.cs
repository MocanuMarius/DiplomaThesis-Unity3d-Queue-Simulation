
//using MathNet.Numerics.Distributions;
using Troschuetz.Random;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Statistics : MonoBehaviour {
	// Use this for initialization
	Distribution distrib;
	public Text mytext;	
	public Toggle myToggle;
	void Start () {
		distrib = new NormalDistribution ();
		myToggle.Select ();
	}
	void Update () {
		Debug.Log (distrib.Variance);
		mytext.text = distrib.NextDouble ().ToString ();
		mytext.text = Time.realtimeSinceStartup.ToString ();
}
}


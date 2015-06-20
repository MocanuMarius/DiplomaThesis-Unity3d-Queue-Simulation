using UnityEngine;
using System.Collections;
using System;

public class DistributionManager : MonoBehaviour {
	// Use this for initialization
    public static int uniformChance = 50;
    public delegate bool SetDistrib();
    public static SetDistrib getDistrib;
	void Awake () {
        getDistrib = getUniform;
	}

	// Update is called once per frame
	void Update () {
	
	}
	public static bool getNormal()
	{
     //mean = 3;sigma=1;
     // Goes from 0 to 6 covering most values
     double x = StateManager.Instance().getPercentElapsed() * 6;
     double pdf = 1/(Math.Sqrt(2*Math.PI))*Math.Pow(Math.E,-(Math.Pow(x-3,2))/2);
     if (pdf > UnityEngine.Random.Range(0f,1f))
         return true;
     else return false;
	}
    public static bool getErlang()
    {
     //k=4,lamda=4
    // Goes from 0 to 2.5 covering most values
        double lamda = 4;
        double k=4;
        double x = StateManager.Instance().getPercentElapsed() * 2.5;
        double pdf = Math.Pow(lamda, k) * Math.Pow(x, k - 1) * Math.Pow(Math.E, -lamda * x) / 6;
        if (pdf > UnityEngine.Random.Range(0f, 1f))
            return true;
        else return false;
    }
    public static bool getTriangle()
    {
        //min=0,max=2
        //Goes from 0 to 2
        double x = StateManager.Instance().getPercentElapsed() * 2;
        double pdf;
        if (x >= 0 && x <= 1) { pdf = x; }
        else pdf = 2-x;
        if (pdf > UnityEngine.Random.Range(0f,1f))
            return true;
        else return false;
    }
    public static bool getUniform()
    {

        if (UnityEngine.Random.Range(0f,100f) < DistributionManager.uniformChance)
            return true;
        else return false;
    }
    public static void setNormal()
    {
        getDistrib = getNormal;
    }
    public static void setUniform()
    {
        getDistrib = getUniform;
    }
    public static void setErlang()
    {
        getDistrib = getErlang;
    }
    public static void setTriangle()
    {
        getDistrib = getTriangle;
    }
}


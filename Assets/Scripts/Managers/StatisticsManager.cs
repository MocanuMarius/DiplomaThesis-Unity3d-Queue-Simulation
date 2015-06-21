using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StatisticsManager : MonoBehaviour {

	// Use this for initialization
    public static float averageWaitTime = 0;  //DDD
    public static float averageInsideTime = 0; //DDD
    public static float totalInsideTime = 0; //DDD
    public static float totalWaitTime = 0; //DDD
    public static float minInsideTime = int.MaxValue;  //DDD
    public static float maxInsideTime = 0; //DDD
    public static float minWaitTime = int.MaxValue; //DDD
    public static float maxWaitTime = 0; //DDD

    public static int personsBought = 0; //DDD
    public static float percentAbandoned = 0; //DDD
    public static int totalAbandoned = 0;  //DDD
    public static int totalLost = 0; //DDD
    public static int totalEntered = 0; //DDD
    public static int nrOfAvailableQueues = 0; //DDD
    public static int totalItemsBought = 0;  //DDD
    public static int totalItemsAbandoned = 0; //DDD
    public static int KPI = 0; //DDD
    public static int simulationTime = 0;

    private static StatisticsManager statisticsManager;
    public static void gatherAllData()
    {
        StateManager.simState = StateManager.SimState.GatheringData;
        nrOfAvailableQueues = 5 - StateManager.Instance().numberOfDisabledQueues ;
        totalEntered = PersonManager.Instance().numberOfPersonsTotal;
        totalAbandoned = PersonManager.Instance().numberOfPersonsAborted;
        totalLost = PersonManager.Instance().numberOfPersonsNotSpawned;
        personsBought = totalEntered - totalAbandoned;
        percentAbandoned = (float)Math.Round(((float)totalAbandoned / (float)totalEntered ) *100,2);
        averageInsideTime = (float)Math.Round((totalInsideTime / totalEntered) /60,2);
        averageWaitTime = (float)Math.Round((totalWaitTime / personsBought) / 60,2);
        totalInsideTime = (float)Math.Round(totalInsideTime / 60,2);
        totalWaitTime = (float)Math.Round(totalWaitTime / 60,2);
        minInsideTime = (float)Math.Round(minInsideTime / 60,2);
        maxInsideTime = (float)Math.Round(maxInsideTime / 60,2);
        minWaitTime = (float)Math.Round(minWaitTime / 60,2);
        maxWaitTime = (float)Math.Round(maxWaitTime / 60,2);
        simulationTime = (int)StateManager.Instance().timeHorizonSeconds / 60;

    }
    //TO BE PUTON PERSON DESTROY
    public static void gatherPersonData(Person person)
    {
        if (!person.hasAbandoned)
          totalWaitTime += person.totalTimeInQueue;
        if (!person.hasAbandoned)
          totalInsideTime += person.insideTime;
        //SET MIN AND MAX WAIT TIME
        if (person.totalTimeInQueue < minWaitTime)
            minWaitTime = person.totalTimeInQueue;
        if (person.totalTimeInQueue > maxWaitTime)
            maxWaitTime = person.totalTimeInQueue;

        //SET MIN AND MAX INSIDE TIME
        if (!person.hasAbandoned)
          if (person.insideTime < minInsideTime)
               minInsideTime = person.insideTime;
        if (!person.hasAbandoned)
            if (person.insideTime > maxInsideTime)
              maxInsideTime = person.insideTime;

        //SET TOTAL ITEMS COUNT
        if (person.hasAbandoned)
            totalItemsAbandoned += person.bucketItemCount;
        else
            totalItemsBought += person.bucketItemCount;
    }
    public static StatisticsManager Instance()
    {
        if (!statisticsManager)
        {
            statisticsManager = FindObjectOfType(typeof(StatisticsManager)) as StatisticsManager;
        }
        return statisticsManager;
    }
    public static void showStatisticsAndHideOtherGui()
    {
        Time.timeScale = 0;
        gatherAllData();
        int KPI = (int)Math.Round(
            0.01 * (float)totalEntered + (1000 -Math.Pow( percentAbandoned,2)) - Math.Sqrt(maxWaitTime * averageWaitTime)
            - ((float)totalLost/(float)totalEntered * 100)
            , 0);
        GameObject.FindGameObjectWithTag("canvas_statisticsMenu").GetComponent<Canvas>().enabled = true;
        GameObject.Find("wtiqa").GetComponent<Text>().text = ""+averageWaitTime;
        GameObject.Find("wtia").GetComponent<Text>().text = "" + averageInsideTime;
        GameObject.Find("wtiqmin").GetComponent<Text>().text = "" + minWaitTime;
        GameObject.Find("wtiamin").GetComponent<Text>().text = "" + minInsideTime;
        GameObject.Find("wtiqmax").GetComponent<Text>().text = "" + maxWaitTime;
        GameObject.Find("wtiamax").GetComponent<Text>().text = "" + maxInsideTime;
        GameObject.Find("tae").GetComponent<Text>().text = "" + totalEntered;
        GameObject.Find("taa").GetComponent<Text>().text = "" + totalAbandoned;
        GameObject.Find("tal").GetComponent<Text>().text = "" + totalLost;
        GameObject.Find("a").GetComponent<Text>().text = "" + percentAbandoned;
        GameObject.Find("tib").GetComponent<Text>().text = "" + totalItemsBought;
        GameObject.Find("tia").GetComponent<Text>().text = "" + totalItemsAbandoned;
        GameObject.Find("qu").GetComponent<Text>().text = "" + nrOfAvailableQueues;
        GameObject.Find("st").GetComponent<Text>().text = "" + simulationTime;
        GameObject.Find("kpi").GetComponent<Text>().text = ""+KPI;
        StateManager.simState = StateManager.SimState.Finished;
    }
}

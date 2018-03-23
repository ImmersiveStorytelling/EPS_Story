using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MeasureFrameDelayScript : MonoBehaviour {

    Stopwatch swIntervals = new Stopwatch();
    long total = 0;
    long average = 0;
    int frameNumber = 0;
    long highestDelay = 0;
    long fpsToAim = 0;


	void Start () {
        UnityEngine.Debug.Log("Start");
        swIntervals.Start();
	}


	void Update () {
        swIntervals.Stop();
        frameNumber++;

        UnityEngine.Debug.Log("frame: " + frameNumber + " + elapsed time in ms: " + swIntervals.ElapsedMilliseconds);

        total += swIntervals.ElapsedMilliseconds;
        average = total / frameNumber;

        UnityEngine.Debug.Log("average: " + average);

        if (swIntervals.ElapsedMilliseconds > highestDelay)
            highestDelay = swIntervals.ElapsedMilliseconds;

        UnityEngine.Debug.Log("highestDelay: " + highestDelay);

        fpsToAim = 1000 / average;

        UnityEngine.Debug.Log("fpsToAim: " + fpsToAim);

        swIntervals.Reset();
        swIntervals.Start();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    public VideoPlayer VideoPlayer;
    public int AmountOfVideos;
    public int AmountOfDelayBetweenVideosInMs;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        runSequenceOfVideos(AmountOfDelayBetweenVideosInMs, AmountOfVideos);
	}

    private void runSequenceOfVideos(int delay, int amountOfVideos)
    {
        if (stopwatch.IsRunning)
        {
            if (stopwatch.ElapsedMilliseconds > delay)
            {
                currentVideo++;
                //if (currentVideo == 5)
                //    activate runScript for mirror
                if (currentVideo >= amountOfVideos)
                    currentVideo = 0;

                startTakeNumber(currentVideo);
                stopwatch.Reset();          
                stopwatch.Start();

                UnityEngine.Debug.Log("currentVideo: " + currentVideo);
            }
        }
        else
        {
            startTakeNumber(currentVideo);
            stopwatch.Start();
        }
    }

    private void startTakeNumber(int numberOfTake)
    {
        VideoPlayer.url = "Assets/Footage/" + numberOfTake.ToString() + ".MP4";
        VideoPlayer.Play();
    }


    int currentVideo = 0;
    Stopwatch stopwatch = new Stopwatch();
}

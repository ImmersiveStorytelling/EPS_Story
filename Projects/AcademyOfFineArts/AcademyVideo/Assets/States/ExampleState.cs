using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class ExampleState: AbstractState
{
    public ExampleState(VideoPlayer videoPlayer, int stateID, string nameOfVideo, int timeOfVideoInMs)
    {
        this.VideoPlayer = videoPlayer;
        this.StateID = stateID;
        this.nameOfVideo = nameOfVideo;
        this.timeOfVideoInMs = timeOfVideoInMs;

        StateFinished = false;
    }

    public override void RunState()
    {
        UnityEngine.Debug.Log("state " + StateID);

        if (!stopwatch.IsRunning)
            stopwatch.Start();
        else
        {
            if (stopwatch.ElapsedMilliseconds > 2000)
            {
                stopwatch.Stop();
                stopwatch.Reset();
                StateFinished = true;
            }
            //else
            //    UnityEngine.Debug.Log("time in ms elapsed: " + stopwatch.ElapsedMilliseconds);
        }
    }

    public override bool IsFinished()
    {
        return StateFinished;
    }

    public override void StopState()
    {
        StateFinished = false;
    }

    string nameOfVideo;
    int timeOfVideoInMs;
    Stopwatch stopwatch = new Stopwatch();
}

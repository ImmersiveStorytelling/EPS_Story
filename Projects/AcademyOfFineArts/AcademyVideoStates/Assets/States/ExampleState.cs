using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class ExampleState: AbstractState
{
    public ExampleState(int stateID, VideoPlayer videoPlayer, string nameOfVideo)
    {
        _stateID = stateID;
        _nameOfVideo = nameOfVideo;
        _videoPlayer = videoPlayer;

        _stateFinished = false;
    }

    public override void RunState()
    {
        UnityEngine.Debug.Log("state " + _stateID);

        if (stopwatch.ElapsedMilliseconds >= 2000)
        {
            stopwatch.Stop();
            stopwatch.Reset();
            _stateFinished = true;
        }
        else
            UnityEngine.Debug.Log("time in ms elapsed: " + stopwatch.ElapsedMilliseconds);
    }

    public override void StartState()
    {
        base.StartState();
        stopwatch.Start();
    }

    Stopwatch stopwatch = new Stopwatch();
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class StatePlayVideo: AbstractState
{
    public StatePlayVideo(int stateID, VideoPlayer videoPlayer, string nameOfVideo, int timeOfVideoInMs)
    {
        _stateID = stateID;
        _nameOfVideo = nameOfVideo;
        _videoPlayer = videoPlayer;
        this.timeOfVideoInMs = timeOfVideoInMs;

        _stateFinished = false;
    }

    public override void RunState()
    {
        UnityEngine.Debug.Log("state " + _stateID);

        if (_videoPlayer.time * 1000 >= timeOfVideoInMs)
        {
            _stateFinished = true;
        }
        else
            UnityEngine.Debug.Log("time in ms elapsed: " + (int)(_videoPlayer.time * 1000));
    }

    public override bool IsFinished()
    {
        return _stateFinished;
    }

    public override void StopState()
    {
        _stateFinished = false;
    }

 
    int timeOfVideoInMs;
}

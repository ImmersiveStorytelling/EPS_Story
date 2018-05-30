using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class ExampleState: AbstractState
{
    string video;
    VideoPlayer videoPlayer;
    public ExampleState(int stateID, VideoPlayer videoPlayer)
    {
        this.stateID = stateID;
        stateFinished = false;
        video = "test1";
        this.videoPlayer = videoPlayer;
    }

    //public void PlayVideoByName(VideoPlayer videoPlayer, string nameOfVideo)
    //{
    //    videoPlayer.url = "Assets/Footage/" + nameOfVideo + ".MP4";
    //    videoPlayer.Play();
    //}
    public void StartState()
    {
        PlayVideoByName(videoPlayer, video);
    }

    public override void RunState()
    {
        UnityEngine.Debug.Log("state " + stateID);

        if (!stopwatch.IsRunning)
            stopwatch.Start();
        else
        {
            if (stopwatch.ElapsedMilliseconds > 2000)
            {
                stopwatch.Stop();
                stopwatch.Reset();
                stateFinished = true;
            }
            //else
            //    UnityEngine.Debug.Log("time in ms elapsed: " + stopwatch.ElapsedMilliseconds);
        }
    }

    public override bool IsFinished()
    {
        return stateFinished;
    }

    public override void StopState()
    {
        stateFinished = false;
    }

    Stopwatch stopwatch = new Stopwatch();
}

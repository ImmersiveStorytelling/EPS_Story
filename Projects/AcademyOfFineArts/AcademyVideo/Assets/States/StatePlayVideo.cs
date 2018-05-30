using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class StateVideoPlay : AbstractState
{
    public StateVideoPlay(int stateID, VideoPlayer videoplayer, string nameOfVideo, int timeOfVideoInMs)
    {
        this.StateID = stateID;
        this.VideoPlayer = videoplayer;
        this.nameOfVideo = nameOfVideo;
        this.timeOfVideoInMs = timeOfVideoInMs;

        StateFinished = false;
    }

    public override void RunState()
    {
        UnityEngine.Debug.Log("state " + StateID);

        if (!VideoPlayer.isPlaying)
        {
            PlayVideoByName(VideoPlayer, nameOfVideo);
        }
        else
        {
            UnityEngine.Debug.Log("Time video: " + VideoPlayer.time);
            if (VideoPlayer.time > 2)
            {
                VideoPlayer.Stop();
                StateFinished = true;
            }
        }



        /*if (!stopwatch.IsRunning)
        {
            PlayVideoByName(VideoPlayer, nameOfVideo);
            stopwatch.Start();
        }
            
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
        }*/
    }

    public override bool IsFinished()
    {
        VideoPlayer.Stop();
        return StateFinished;
    }

    public override void StopState()
    {
        StateFinished = false;
    }

    Stopwatch stopwatch = new Stopwatch();
    string nameOfVideo;
    int timeOfVideoInMs;
}

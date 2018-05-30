using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class AbstractState {

    public int stateID;
    public bool stateFinished = false;

    //In this method you will have to set the bool variable 'stateFinished' when the state is finished, 
    //so that you can switch to the next state.
    public abstract void RunState();

    //With this method we can call up if states it finished to switch to next state, 
    //or if it still running.
    public abstract bool IsFinished();

    //With this method we will redefine all necessary variable to stop and reset the state to be able to switch to the next state and reuse this state.
    public abstract void StopState();

    //This method will be used to check if all parameters exist or if they are set properly. 
    //This can be used to override and check for each state separate, which then can be run by the manager script to run over all methods.
    public virtual bool CheckParameters()
    {
        if (!stateFinished)
            return true;
        else
        {
            UnityEngine.Debug.LogWarning("Parameter 'stateFinished' is set on 'true' before entering stage.");
            return false;
        }
    }

    public void PlayVideoByName(VideoPlayer videoPlayer, string nameOfVideo)
    {
        videoPlayer.url = "Assets/Footage/" + nameOfVideo + ".MP4";
        videoPlayer.Play();
    }
}

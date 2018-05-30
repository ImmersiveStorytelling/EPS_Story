using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    public VideoPlayer VideoPlayer;

	// Use this for initialization
	void Start () {
        setStates();
        checkParametersOfStates();
        setStartState();
        currentState.PlayVideoByName(VideoPlayer, "test1");
    }
	
	// Update is called once per frame
	void Update () {
        
        if (currentState.IsFinished())
        {
            changeToNextState();
            currentState.RunState();
            currentState.PlayVideoByName(VideoPlayer, "test2");
        }
        currentState.RunState();
    }

    private void setStates()
    {
        states = new AbstractState[amountOfStates];
        states[0] = new StateVideoPlay(0, VideoPlayer, "test1", 2000);
        states[1] = new StateVideoPlay(1, VideoPlayer, "test2", 2000);
        states[2] = new StateVideoPlay(2, VideoPlayer, "test1", 2000);
        states[3] = new StateVideoPlay(3, VideoPlayer,  "test2", 2000);
    }
    private void checkParametersOfStates()
    {
        for (int i = 0; i < amountOfStates; i++)
        {
            if (!states[i].CheckParameters())
                Debug.LogError("Parameters of state " + states[i] + " are set wrong or are incomplete.");
        }
    }
    private void setStartState()
    {
        currentStateNumber = 0;
        currentState = states[currentStateNumber];
    }

    private void changeToNextState()
    {
        currentState.StopState();

        if (currentStateNumber < amountOfStates - 1)
            currentStateNumber++;
        else
            currentStateNumber = 0;

        currentState = states[currentStateNumber];
    }

    //private void PlayVideoByName(string nameOfVideo)
    //{
    //    VideoPlayer.url = "Assets/Footage/" + nameOfVideo + ".MP4";
    //    VideoPlayer.Play();
    //}

    AbstractState[] states;
    int amountOfStates = 4;
    int currentStateNumber;
    AbstractState currentState;

}

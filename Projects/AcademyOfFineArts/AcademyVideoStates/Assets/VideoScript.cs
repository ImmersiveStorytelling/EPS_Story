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
        currentState.StartState();
    }
	
	// Update is called once per frame
	void Update () {

        if (currentState.IsFinished())
        {
            changeToNextState();
            currentState.StartState();
        }
        currentState.RunState();
    }

    private void setStates()
    {
        states = new AbstractState[amountOfStates];
        states[0] = new StatePlayVideo(0, VideoPlayer, "test1", 2000);
        states[1] = new MirrorState(1, VideoPlayer, "test2", 2000);
        //states[2] = new StatePlayVideo(2, VideoPlayer, "welding", 2000);
        amountOfStates = states.Length;
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

    AbstractState[] states;
    int amountOfStates = 2;
    int currentStateNumber;
    AbstractState currentState;

}

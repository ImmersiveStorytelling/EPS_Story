using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    public VideoPlayer VideoPlayer;
    ExampleState state = new ExampleState(0);

	// Use this for initialization
	void Start () {
        //setStates();
        //checkParametersOfStates();
        //setStartState();
        //PlayVideoByName("test1");
        state.PlayVideoByName(VideoPlayer, "test2");
    }
	
	// Update is called once per frame
	void Update () {

        //if (currentState.IsFinished())
        //{
        //    changeToNextState();
        //    currentState.RunState();
        //}
        //currentState.RunState();
    }

    private void setStates()
    {
        states = new AbstractState[amountOfStates];
        states[0] = new ExampleState(0);
        states[1] = new ExampleState(1);
        states[2] = new ExampleState(2);
        states[3] = new ExampleState(3);
        states[4] = new ExampleState(4);
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

    private void PlayVideoByName(string nameOfVideo)
    {
        VideoPlayer.url = "Assets/Footage/" + nameOfVideo + ".MP4";
        VideoPlayer.Play();
    }

    AbstractState[] states;
    int amountOfStates = 5;
    int currentStateNumber;
    AbstractState currentState;

}

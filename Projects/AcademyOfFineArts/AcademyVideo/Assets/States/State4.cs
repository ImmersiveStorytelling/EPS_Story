using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class State4: AbstractState
{
    public State4(int stateID)
    {
        this.stateID = stateID;
        stateFinished = false;
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
            else
                UnityEngine.Debug.Log("time in ms elapsed: " + stopwatch.ElapsedMilliseconds);
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

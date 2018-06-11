using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



public class VideoScript : MonoBehaviour
{

    public VideoPlayer VideoPlayer;

    // Use this for initialization
    void Start()
    {
        startTakeNumber(0);
    }

    // Update is called once per frame
    void Update()
    {
        updateVariables();
        checkAngles();
    }

    private void startTakeNumber(int numberOfTake)
    {
        //if(VideoPlayer.isPlaying) VideoPlayer.Stop();

        VideoPlayer.url = "Assets/Footage/" + numberOfTake.ToString() + ".MP4";
        VideoPlayer.Play();
    }

    private void updateVariables()
    {
        //vAngle = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAngle = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        Debug.Log(vAngle); 
    }

    private void checkAngles()
    {
        if (Mathf.Abs(vAngle.x - idealRotation.x) < vTriggerAngle.x && Mathf.Abs(vAngle.y - idealRotation.y) < vTriggerAngle.y && !switched)
        {
            startTakeNumber(1);
            switched = true;
        }
    }


    Vector3 vAngle = new Vector3(); //angles headset
    Vector3 vTriggerAngle = new Vector3(45, 45, 0);
    Vector3 idealRotation = new Vector3(0f, 180f, 0f);
    bool switched = false;

}

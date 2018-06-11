using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



public class MultipleShotsOnAngles: MonoBehaviour
{

    public VideoPlayer VideoPlayer;

    // Use this for initialization
    void Start()
    {
        startTakeNumber(1);
    }

    // Update is called once per frame
    void Update()
    {
        updateVariables();
    }

    private void startTakeNumber(int numberOfTake)
    {
        VideoPlayer.url = "Assets/Footage/" + numberOfTake.ToString() + ".MP4";
        VideoPlayer.Play();
    }

    private void updateVariables()
    {
        //vAngle = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAngle = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        Debug.Log(vAngle);

        if (Mathf.Abs(vAngle.x - idealRotation.x) < 45 && Mathf.Abs(vAngle.y - idealRotation.y) < 45 && !switched)
        {
            startTakeNumber(2);
            switched = true;
        }
    }


    Vector3 vAngle = new Vector3(); //angles headset
    Vector3 idealRotation = new Vector3(0f, 180f, 0f);
    bool switched = false;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    public VideoPlayer VideoPlayer;
    public Vector3 vz = new Vector3(); //zero point where raster starts

    // Use this for initialization
    void Start () {
        setupVariables();
        startTakeNumber(1);
	}
	
	// Update is called once per frame
	void Update () {
        updateVariables();
        
    }

    private void startTakeNumber(int numberOfTake)
    {
        VideoPlayer.url = "Assets/Footage/" + numberOfTake.ToString() + ".MP4";
        VideoPlayer.Play();
    }

    private void setupVariables()
    {
        v0.x = 180;
        v0.y = 150;
    }

    private void updateVariables()
    {
        //vAnglesHeadset = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        v = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        UnityEngine.Debug.Log("X: " + v.x + " Y: " + v.y);
    }

    private void checkHeadFrame()
    {
        //if (v.x > ())
    }

    Vector3 v = new Vector3(); //angles headset
    Vector2 v0 = new Vector2(); //amount of dergees of turning head; sideways spread of 180°; up/downwards spread of 150°

}

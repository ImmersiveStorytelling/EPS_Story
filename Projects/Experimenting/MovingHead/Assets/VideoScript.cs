using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    public VideoPlayer VideoPlayer;
    public Vector3 vRasterZeroPoint = new Vector3(); //zero point where raster starts
    public Vector2Int headNumberOfFrames = new Vector2Int(); //states the number of frames in rows and columns of head frames

    // Use this for initialization
    void Start () {
        setupVariables();
        startTakeNumber(1);
	}
	
	// Update is called once per frame
	void Update () {
        updateVariables();
        setHeadFrame();
    }

    private void startTakeNumber(int numberOfTake)
    {
        VideoPlayer.url = "Assets/Footage/" + numberOfTake.ToString() + ".MP4";
        VideoPlayer.Play();
    }

    private void setupVariables()
    {
        vSpread.x = 180;
        vSpread.y = 150;
    }

    private void updateVariables()
    {
        //vAnglesHeadset = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAngleHeadset = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK

        UnityEngine.Debug.Log("vAngleHeadset: X: " + vAngleHeadset.x + " Y: " + vAngleHeadset.y);
    }

    private void setHeadFrame()
    {
        headFrameToShowYZ.x = setFrameNumberOfParameter(vAngleHeadset.x, vSpread.x, vRasterZeroPoint.x, headNumberOfFrames.x);
        headFrameToShowYZ.y = setFrameNumberOfParameter(vAngleHeadset.y, vSpread.y, vRasterZeroPoint.y, headNumberOfFrames.y);


        UnityEngine.Debug.Log("HeadFrame to play: x" + headFrameToShowYZ.x + "y" + headFrameToShowYZ.y + ".png");
        //playHeadFrame(headFrameToShowYZ);
    }

    private int setFrameNumberOfParameter(double vAngle, double vSpread, double vRasterZeroPoint, int numberOfFrames)
    {
        if (vAngle > (vRasterZeroPoint + vSpread))
            return numberOfFrames;
        else if (vAngle < vRasterZeroPoint)
            return 0;
        else
        {
            return (int)((vAngle - vSpread) / (vSpread / numberOfFrames));
        }
    }

    Vector3 vAngleHeadset = new Vector3(); //angles headset
    Vector2 vSpread = new Vector2(); //amount of dergees of turning head; sideways spread of 180°; up/downwards spread of 150°
    Vector2Int headFrameToShowYZ = new Vector2Int(); //this vector will hold the values of which to select the correct frame from
}

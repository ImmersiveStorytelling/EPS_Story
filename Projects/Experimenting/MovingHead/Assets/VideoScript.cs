using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    public VideoPlayer VideoPlayer;
    public Vector3 Vzp = new Vector3(); //zero point where raster starts
    public Vector2Int NumberOfFramesHead = new Vector2Int(); //states the number of frames in rows and columns of head frames

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
        vSp.x = 150;
        vSp.y = 180;
    }

    private void updateVariables()
    {
        //vAnglesHeadset = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAngle = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        UnityEngine.Debug.Log("vAngleHeadset: X: " + vAngle.x + " Y: " + vAngle.y);

        vRefUpper.x = Vzp.x + vSp.x;
        vRefLower.x = Vzp.x;
        if (vRefUpper.x > 359)
        {
            vRefUpper.x = vRefUpper.x - 360;
        }

        //UnityEngine.Debug.Log("ZP.x: " + Vzp.x + " and SP.x: " + vSp.x);
        //UnityEngine.Debug.Log("vRefMax.x: " + vRefUpper.x + " and vRefLow.x: " + vRefLower.x);

        vRefUpper.y = Vzp.y + vSp.y;
        vRefLower.y = Vzp.y;
        if (vRefUpper.y > 359)
        {
            vRefUpper.y = vRefUpper.y - 360;
        }

        //UnityEngine.Debug.Log("ZP.y: " + Vzp.y + " and SP.y: " + vSp.y);
        //UnityEngine.Debug.Log("vRefMax.y: " + vRefUpper.y + " and vRefLow.y: " + vRefLower.y);
    }

    private void setHeadFrame()
    {
        //headFrameToShowYZ.x = setFrameNumberOfParameter(vAngleHeadset.x, vSpread.x, vRasterZeroPoint.x, headNumberOfFrames.x);
        //headFrameToShowYZ.y = setFrameNumberOfParameter(vAngleHeadset.y, vSpread.y, vRasterZeroPoint.y, headNumberOfFrames.y);


        //UnityEngine.Debug.Log("HeadFrame to play: x" + headFrameToShowYZ.x + "y" + headFrameToShowYZ.y + ".png");
        //playHeadFrame(headFrameToShowYZ);
        
        if (vRefUpper.x > vRefLower.x)
        {
            if (vAngle.x > vRefUpper.x)
            {
                UnityEngine.Debug.Log("difference: " + ((vAngle.x - vRefUpper.x) + ((360-vAngle.x) + vRefLower.x)));
            }
            else if (vAngle.x < vRefLower.x)
            {
                UnityEngine.Debug.Log("INSIDE");
            }
            else
            {
                UnityEngine.Debug.Log("difference: " + (((360 - vRefUpper.x) + vAngle.x) + (vRefLower.x - vAngle.x)));
            }
        }
        else if (vRefUpper.x <= vRefLower.x)
        {
            if ((vAngle.x < vRefLower.x) && (vAngle.x > vRefUpper.x))
            {
                UnityEngine.Debug.Log("difference: " + ((vAngle.x - vRefUpper.x) + (vRefLower.x - vAngle.x)));
            }
            else
            {
                UnityEngine.Debug.Log("INSIDE");
            }
        }

        if (vRefUpper.y > vRefLower.y)
        {
            if (vAngle.y > vRefUpper.y)
            {
                UnityEngine.Debug.Log("difference: " + ((vAngle.y - vRefUpper.y) + ((360 - vAngle.y) + vRefLower.y)));
            }
            else if (vAngle.y < vRefLower.y)
            {
                UnityEngine.Debug.Log("INSIDE");
            }
            else
            {
                UnityEngine.Debug.Log("difference: " + (((360 - vRefUpper.y) + vAngle.y) + (vRefLower.y - vAngle.y)));
            }
        }
        else if (vRefUpper.y <= vRefLower.y)
        {
            if ((vAngle.y < vRefLower.y) && (vAngle.y > vRefUpper.y))
            {
                UnityEngine.Debug.Log("difference: " + ((vAngle.y - vRefUpper.y) + (vRefLower.y - vAngle.y)));
            }
            else
            {
                UnityEngine.Debug.Log("INSIDE");
            }
        }
    }


    private int setFrameNumberOfParameter(double vAngle, double vSpread, double vRasterZeroPoint, int numberOfFrames)
    {
        return 0;
    }

    Vector3 vAngle = new Vector3(); //angles headset
    Vector2 vSp = new Vector2(); //amount of dergees of turning head; sideways spread of 180°; up/downwards spread of 150°
    Vector2 vRefUpper = new Vector2(); //point to reference to upper boundary => Vzp + vSp
    Vector2 vRefLower = new Vector2(); //point to reference to lower boundary => Vzp
    Vector2Int headFrameToShowYZ = new Vector2Int(); //this vector will hold the values of which to select the correct frame from
}

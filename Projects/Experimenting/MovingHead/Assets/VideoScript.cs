using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {

    public VideoPlayer VideoPlayer;
    public Vector3 Vzp = new Vector3(); //zero point where raster starts
    public Vector2 Vsp = new Vector2(); //amount of dergees of turning head; sideways spread of 180°; up/downwards spread of 150°
    public Vector2Int NumberOfFrames = new Vector2Int(); //states the number of frames in rows and columns of head frames

    // Use this for initialization
    void Start () {
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

    private void updateVariables()
    {
        //vAnglesHeadset = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAngle = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        UnityEngine.Debug.Log("vAngleHeadset: X: " + vAngle.x + " Y: " + vAngle.y);

        setBoundaries_mimicingMirrorRaster();
    }
    private void setBoundaries_mimicingMirrorRaster()
    {
        vRefLower.x = Vzp.x;
        vRefUpper.x = setUpperBoundary(vRefLower.x, Vzp.x, Vsp.x);
        vRefLower.y = Vzp.y;
        vRefUpper.y = setUpperBoundary(vRefLower.y, Vzp.y, Vsp.y);
    }
    private float setUpperBoundary(float low, float zp, float sp)
    {
        float up = low + sp;
        if (up > 359)
        {
            up = up - 360;
        }
        return up;
    }

    private void setHeadFrame()
    {
        UnityEngine.Debug.Log("Frame x: " + checkRasterForFrame(vRefUpper.x, vRefLower.x, vAngle.x, NumberOfFrames.x));
        UnityEngine.Debug.Log("Frame y: " + checkRasterForFrame(vRefUpper.y, vRefLower.y, vAngle.y, NumberOfFrames.y));

        //headFrameToShowYZ.x = setFrameNumberOfParameter(vAngleHeadset.x, vSpread.x, vRasterZeroPoint.x, headNumberOfFrames.x);
        //headFrameToShowYZ.y = setFrameNumberOfParameter(vAngleHeadset.y, vSpread.y, vRasterZeroPoint.y, headNumberOfFrames.y);


        //UnityEngine.Debug.Log("HeadFrame to play: x" + headFrameToShowYZ.x + "y" + headFrameToShowYZ.y + ".png");
        //playHeadFrame(headFrameToShowYZ);
    }
    private int checkRasterForFrame(float vRefUpper, float vRefLower, float vAngle, int numberOfFrames) //return float frame number
    {
        if (vRefUpper > vRefLower) //raster between 0 - 360 degrees boundaries
        {
            if ((vAngle < vRefUpper) && (vAngle > vRefLower))
                return selectFrameFromRaster();
            else
            {
                if (vAngle > vRefUpper)
                {
                    if ((vAngle - vRefUpper) < ((360 - vAngle) + vRefLower))
                        return numberOfFrames;
                    else
                        return 0;
                }
                else
                {
                    if (((360 - vRefUpper) + vAngle) < (vRefLower - vAngle))
                        return numberOfFrames;
                    else
                        return 0;
                }
            }
        }
        else //raster crossing 0 - 360 boundaries
        {
            if ((vAngle < vRefLower) && (vAngle > vRefUpper))
            {
                if ((vAngle - vRefUpper) < (vRefLower - vAngle))
                    return numberOfFrames;
                else
                    return 0;
            }
            else
                return selectFrameFromRaster();
        }
    }
    private int selectFrameFromRaster()
    {
        return 999;
    }


    private int setFrameNumberOfParameter(double vAngle, double vSpread, double vRasterZeroPoint, int numberOfFrames)
    {
        return 0;
    }

    Vector3 vAngle = new Vector3(); //angles headset
    Vector2 vRefUpper = new Vector2(); //point to reference to upper boundary => Vzp + vSp
    Vector2 vRefLower = new Vector2(); //point to reference to lower boundary => Vzp
    Vector2Int headFrameToShowYZ = new Vector2Int(); //this vector will hold the values of which to select the correct frame from
}

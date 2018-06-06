using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    public int BeginTime_Mirror_TotalMs;
    public int EndTime_Mirror_TotalMs;

    public VideoPlayer VideoPlayer;
    public Vector3 Vzp = new Vector3(); //zero point where raster starts
    public Vector2 Vsp = new Vector2(); //amount of dergees of turning head
    public Vector2Int NumberOfFrames = new Vector2Int(); //states the number of frames in rows and columns of head frames

    ImageLoaderScript imageloader;

    // Use this for initialization
    void Start()
    {
        startTakeNumber(1);
        imageloader = GetComponent<ImageLoaderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        updateVariables();
        if (VideoPlayer.time * 1000 >= BeginTime_Mirror_TotalMs && VideoPlayer.time*1000 >= EndTime_Mirror_TotalMs)
        {
            setHeadFrame();
        }
    }

    private void startTakeNumber(int numberOfTake)
    {
        VideoPlayer.url = "Assets/Footage/" + numberOfTake.ToString() + ".MP4";
        VideoPlayer.Play();
    }

    private void updateVariables()
    {
        vAngle = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        //vAngle = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK

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
        mimicFrame.x = checkRasterForFrame(vRefUpper.x, vRefLower.x, vAngle.x, NumberOfFrames.x, Vsp.x);
        mimicFrame.y = checkRasterForFrame(vRefUpper.y, vRefLower.y, vAngle.y, NumberOfFrames.y, Vsp.y);

        playMimicFrame(mimicFrame);
    }
    private void playMimicFrame(Vector2Int mimicFrame)
    {
        string frameToPlay = "x" + mimicFrame.x + "y" + mimicFrame.y + ".png";
        //UnityEngine.Debug.Log(frameToPlay);

        imageloader.loadImage(mimicFrame);
    }
    private int checkRasterForFrame(float vRefUpper, float vRefLower, float vAngle, int numberOfFrames, float spread) //return float frame number
    {
        if (vRefUpper > vRefLower) //raster between 0 - 360 degrees boundaries
        {
            if ((vAngle < vRefUpper) && (vAngle > vRefLower))
                return selectFrameFromRaster(vRefUpper, vRefLower, vAngle, numberOfFrames, spread);
            else
            {
                if (vAngle > vRefUpper)
                {
                    if ((vAngle - vRefUpper) < ((360 - vAngle) + vRefLower))
                        return numberOfFrames - 1;
                    else
                        return 0;
                }
                else
                {
                    if (((360 - vRefUpper) + vAngle) < (vRefLower - vAngle))
                        return numberOfFrames - 1;
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
                    return numberOfFrames - 1;
                else
                    return 0;
            }
            else
                return selectFrameFromRaster(vRefUpper, vRefLower, vAngle, numberOfFrames, spread);
        }
    }
    private int selectFrameFromRaster(float vRefUpper, float vRefLower, float vAngle, int numberOfFrames, float spread)
    {
        if ((vRefUpper > vRefLower) || (vAngle > vRefLower))
            return numberOfFrames - 1 - (int)((numberOfFrames * (vAngle - vRefLower)) / spread);
        else
        {
            return numberOfFrames - 1 - (int)((numberOfFrames * (vAngle + (360 - vRefLower))) / spread);
        }
    }

    Vector3 vAngle = new Vector3(); //angles headset
    Vector2 vRefUpper = new Vector2(); //point to reference to upper boundary => Vzp + vSp
    Vector2 vRefLower = new Vector2(); //point to reference to lower boundary => Vzp
    Vector2Int mimicFrame = new Vector2Int(); //this vector will hold the values of which to select the correct frame from
}

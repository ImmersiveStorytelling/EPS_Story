using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SwitchShotsInRectangles : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<ZoomPictureLocation> zoomPictureLocations = new List<ZoomPictureLocation>();

    [Serializable]
    public class ZoomPictureLocation
    {
        public Vector2 Vector_ZeroPoint = new Vector2(); //zero point where raster starts
        public Vector2 Vector_Spread = new Vector2(); //amount of dergees of turning head
    }

    // Use this for initialization
    void Start()
    {
        startTakeNumber(activeVideo);
    }

    // Update is called once per frame
    void Update()
    {
        updateVariables();
        for (int i = 0; i < zoomPictureLocations.Count; i++)
        {
            if (detector.checkForHitLocation(zoomPictureLocations[i].Vector_ZeroPoint, zoomPictureLocations[i].Vector_Spread, vAngle))
            {
                Debug.Log("IN: " + i);
                if (activeVideo != i)
                {
                    activeVideo = i;
                    startTakeNumber(activeVideo);
                }  
            }
        }
    }

    private void updateVariables()
    {
        //vAngle = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAngle = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        //Debug.Log("vAngle X: " + vAngle.x + " Y: " + vAngle.y);
    }
    private void startTakeNumber(int numberOfTake)
    {
        videoPlayer.url = "Assets/Footage/" + numberOfTake.ToString() + ".MP4";
        videoPlayer.Play();
    }

    int activeVideo = 0;
    ZoomPictureDetector detector = new ZoomPictureDetector();
    Vector3 vAngle = new Vector3();
}

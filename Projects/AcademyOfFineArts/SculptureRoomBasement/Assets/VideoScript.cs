using System;
using System.Collections.Generic;
using UnityEngine;

public class VideoScript : MonoBehaviour {

    public List<ZoomPictureLocation> zoomPictureLocations = new List<ZoomPictureLocation>();
    public Camera cam;

    [Serializable]
    public class ZoomPictureLocation
    {
        public Vector2 Vector_ZeroPoint = new Vector2(); //zero point where raster starts
        public Vector2 Vector_Spread = new Vector2(); //amount of dergees of turning head
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        updateVariables();
        for (int i = 0; i < zoomPictureLocations.Count; i++)
        {
            if (detector.checkForHitLocation(zoomPictureLocations[i].Vector_ZeroPoint, zoomPictureLocations[i].Vector_Spread, vAngle))
            {
                if (startTime <= 0)
                {
                    startTime = DateTime.Now.Millisecond;
                    Debug.Log("IN");
                }

                currentTime = DateTime.Now.Millisecond - startTime;
                Debug.Log("currentTime: " + currentTime);

                if (currentTime > durationForShowingPicture)
                {
                    //Debug.Log("object of picture to zoom on: " + i); //zoom in location
                    //Debug.Log("Time until zooming in: " + startTime);
                    Debug.Log("FIRE");
                    //load image to show
                }


            }
            else
            {
                startTime = 0;
                Debug.Log("OUT");
            } 
        }
	}

    private void updateVariables()
    {
        vAngle = cam.gameObject.transform.rotation.eulerAngles;
        //Debug.Log("vAngle X: " + vAngle.x + " Y: " + vAngle.y);
    }

    ZoomPictureDetector detector = new ZoomPictureDetector();
    Vector3 vAngle = new Vector3();
    int[] startTime; //time in ms
    int[] currentTime;
    int[] durationForShowingPicture = 5000; //2 seconds
}

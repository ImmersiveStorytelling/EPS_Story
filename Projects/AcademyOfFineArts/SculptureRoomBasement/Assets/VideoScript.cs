using System;
using System.Collections.Generic;
using UnityEngine;

public class VideoScript : MonoBehaviour {

    public List<ZoomPictureLocation> zoomPictureLocations = new List<ZoomPictureLocation>();

    [Serializable]
    public class ZoomPictureLocation
    {
        public Vector2 Vector_ZeroPoint = new Vector2(); //zero point where raster starts
        public Vector2 Vector_Spread = new Vector2(); //amount of dergees of turning head
    }

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        updateVariables();
        for (int i = 0; i < zoomPictureLocations.Count; i++)
        {
            if (detector.checkForHit(zoomPictureLocations[i].Vector_ZeroPoint, zoomPictureLocations[i].Vector_Spread, vAngle))
            {
                Debug.Log("object of picture to zoom on: " + i); //zoom in location

                camera.fieldOfView = 50;
            }
            else
                camera.fieldOfView = 60;

        }
	}

    private void updateVariables()
    {
        //vAngle = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAngle = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        //Debug.Log("vAngle X: " + vAngle.x + " Y: " + vAngle.y);
    }

    ZoomPictureDetector detector = new ZoomPictureDetector();
    Vector3 vAngle = new Vector3();
    Camera camera;
}

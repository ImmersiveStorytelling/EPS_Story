using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class MeasureHeadMovementScript : MonoBehaviour {

	void Start () {
        createNewCsvFile();
        stopwatch.Start();
	}
	

	void Update () {
        
        updateVariables();
        writeNewLineToCsv(frameNumber, currentTimeInMs, vAnglesHeadSet);
	}

    void updateVariables()
    {
        vAnglesHeadSet = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        //vAnglesHeadSet = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        frameNumber++;
        currentTimeInMs = stopwatch.ElapsedMilliseconds;
    }

    void createNewCsvFile()
    {
        dateTime = System.DateTime.Now.ToString("dd-MM_hhumm");
        StringWriter csvTitle = new StringWriter();
        csvTitle.WriteLine("FrameNumber;Time(ms);xAngle(°);yAngle(°);zAngle(°);Right/Left;Up/Down;TiltRight/TiltLeft");
        csvPath = "Assets/MeasurementData/M_HeadMovement_" + dateTime + ".csv";
        File.AppendAllText(csvPath, csvTitle.ToString());
    }

    void writeNewLineToCsv(int frameNumber, long currentTimeInMs, Vector3 angles)
    {
        UnityEngine.Debug.Log("X: " + angles.x + " Y: " + angles.y + " Z: " + angles.z);
        StringWriter csvTitle = new StringWriter();
        csvTitle.WriteLine(frameNumber + ";" + currentTimeInMs + ";" + (int)angles.x + ";" + (int)angles.y + ";" + (int)angles.z + ";");
        File.AppendAllText(csvPath, csvTitle.ToString());

        setPreviousAngles(angles);
    }

    void setPreviousAngles(Vector3 angles)
    {
        vAnglesHeadsetPrevious = angles;
        UnityEngine.Debug.Log("Previous: X: " + vAnglesHeadsetPrevious.x + " Y: " + vAnglesHeadsetPrevious.y + " Z: " + vAnglesHeadsetPrevious.z);
    }

    Stopwatch stopwatch = new Stopwatch();
    long currentTimeInMs = 0;
    int frameNumber = 0;
    Vector3 vAnglesHeadSet = new Vector3();
    Vector3 vAnglesHeadsetPrevious = new Vector3();
    string dateTime;
    string csvPath;
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class MeasureHeadMovementScript : MonoBehaviour {

    public int FpsOfDevice = 90; //standard

	void Start () {
        fpsOfDevice = getFpsOfDevice(FpsOfDevice);
        msInterval = getMsInterval(fpsOfDevice);
        createNewCsvFile();
        stopwatch.Start();
	}
	

	void Update () {
        
        updateVariables();

        if ((currentTimeInMs - previousTimeInMs) > msInterval)
        {
            writeNewLineToCsv(frameNumber, currentTimeInMs,  (int)vAnglesHeadSet.x, (int)vAnglesHeadSet.y, (int)vAnglesHeadSet.z);
        }
	}

    void updateVariables()
    {
        //vAnglesHeadSet = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles; //FOR STEAM VR WITH HEADSET
        vAnglesHeadSet = GameObject.Find("Camera").transform.rotation.eulerAngles; //FOR SIMULATOR USING VRTK
        frameNumber++;
        currentTimeInMs = stopwatch.ElapsedMilliseconds;
    }

    long getMsInterval(int fpsDevice)
    {
        return 1000 / fpsOfDevice;
    }
    int getFpsOfDevice(int fpsOfDeviceIn)
    {
        if (fpsOfDeviceIn <= 0)
            return 1;
        else if (fpsOfDeviceIn > 200) //5ms intervals at 200fps
            return 200;
        return FpsOfDevice;
    }

    void createNewCsvFile()
    {
        dateTime = System.DateTime.Now.ToString("dd-MM_hhumm");
        StringWriter csvTitle = new StringWriter();
        csvTitle.WriteLine("FrameNumber;Time(ms);xAngle(°);yAngle(°);zAngle(°)");
        csvPath = "Assets/MeasurementData/M_HeadMovement_" + dateTime + ".csv";
        File.AppendAllText(csvPath, csvTitle.ToString());
    }
    void writeNewLineToCsv(int frameNumber, long currentTimeInMs, int xAngle, int yAngle, int zAngle)
    {
        UnityEngine.Debug.Log("X: " + vAnglesHeadSet.x + " Y: " + vAnglesHeadSet.y + " Z: " + vAnglesHeadSet.z);
        StringWriter csvTitle = new StringWriter();
        csvTitle.WriteLine(frameNumber + ";" + currentTimeInMs + ";" + xAngle + ";" + yAngle + ";" + zAngle + ";");
        File.AppendAllText(csvPath, csvTitle.ToString());
    }

    int fpsOfDevice = 90;
    long msInterval = 0;
    Stopwatch stopwatch = new Stopwatch();
    long currentTimeInMs = 0;
    long previousTimeInMs = 0;
    int frameNumber = 0;
    Vector3 vAnglesHeadSet = new Vector3();
    string dateTime;
    string csvPath;
}

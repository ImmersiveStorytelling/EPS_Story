using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageLoaderScript : MonoBehaviour
{
    public Vector2Int imageResolution;
    public Material overlay;

    Texture2D[,] images;
    VideoScript vs;
    Vector2Int frames;

    // Use this for initialization
    void Start()
    {
        vs = GetComponent<VideoScript>();
        frames = vs.NumberOfFrames;
        images = new Texture2D[frames.x, frames.y];


        DirectoryInfo d = new DirectoryInfo("Assets/Footage/Mirror/");
        FileInfo[] Files = d.GetFiles("*.png"); //Getting Image Files
        foreach (FileInfo file in Files)
        {
            string fn = file.Name;
            string[] t1 = fn.Split(')');
            //Debug.Log(t1[0]);
            string[] t2 = t1[0].Split('(');

            int x = 0;
            int y = int.Parse(t2[1]);
            Debug.Log("FullName: " + file.FullName);
            images[x, y - 1] = LoadPNG(file.FullName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadImage(Vector2Int xy)
    {
        string filename = "pic (" + (xy.y + 1).ToString() + ").png";
        string filepath = "Assets/Footage/Mirror/" + filename;
        Debug.Log(filepath);


        if (images[0, xy.y] != null)
        {
            overlay.mainTexture = images[0, xy.y];
        }
        else
        {
            Debug.Log("This image doesn't exist.");
        }

    }

    Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            Debug.Log("File exists");
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(imageResolution.x, imageResolution.y);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        else
        {
            Debug.Log("FileNotFound");
        }
        return tex;
    }
}

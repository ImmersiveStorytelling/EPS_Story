using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWriter {

    //Singleton
    private static FileWriter instance;

    private FileWriter()
    {

    }

    public static FileWriter Instance
    {
        get
        {
            if (instance == null)
                instance = new FileWriter();
            return instance;
        }
    }

    //save data (public)
    //clear data (private)
    //write data (private? need to be fired at end of cycle, does manager do or does he only say true and internally fire?)
			
}

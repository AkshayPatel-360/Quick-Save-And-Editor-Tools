using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryFormatterExample : MonoBehaviour {

   // MiniClass mc;
	// Use this for initialization
	void Start () { }

    public void saveDataToDisk(string filePath, object toSave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.streamingAssetsPath + "/" + filePath;
        //string path2 = Path.Combine()
        FileStream file = File.Create(path);
        bf.Serialize(file, toSave);
        file.Close();
    }

    /**
     * Loads the save data from the disk
     */
    public T LoadDataFromDisk<T>(string filePath)
    {
        T toRet;
        string path = Application.streamingAssetsPath + "/" + filePath;
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            toRet = (T)bf.Deserialize(file);
            file.Close();
        }
        else
            toRet = default(T);
        return toRet;
    }
    
    
}

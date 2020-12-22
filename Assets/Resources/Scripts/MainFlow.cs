using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

public class MainFlow : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] items;
    [SerializeField] public List<GameObject> listOfGameObj;
    public List<Info> listOfInfo;
    public float RandomSpawnCount;
    int SpawnCount; 
    public GameObject groundSize;
    public int randomObject;
    BinaryFormatter bf = new BinaryFormatter();
    

    Vector2 randomSpawnLocation;
    SerializableInfoClass classToSerialize = new SerializableInfoClass();

    void Start()
    {
        //System.Array.Clear(items, 0, items.Length);
        items = Resources.LoadAll("Prefabs",typeof(GameObject)).Cast<GameObject>().ToArray();

        Resources.LoadAll("Textures", typeof(Texture2D)).Cast<Texture2D>().ToArray();

        SpwanRendomObjects();

    }

   
    public void SpwanRendomObjects()
    {
             
        randomObject = Random.Range(items.Length, items.Length);
        
        
        for (int i = 0; i < Random.Range(10, RandomSpawnCount); i++)
        {

            float randX = Random.Range(-groundSize.transform.localScale.x / 2, groundSize.transform.localScale.x / 2 );
            float randY = groundSize.transform.position.y;
            float randZ = Random.Range(-groundSize.transform.localScale.z / 2, groundSize.transform.localScale.z / 2);

            VectorSerialized pos = new VectorSerialized(randX, randY, randZ);
            Vector3 myPos = new Vector3(pos.x, pos.y, pos.z);

            int id = Random.Range(0, 3);
            GameObject objToSpawn = items[id];

            GameObject a = Instantiate(objToSpawn, new Vector3(randX,randY,randZ), Quaternion.identity);
            a.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            a.name = id.ToString();

            listOfGameObj.Add(a);

                     
        }
       
    }


    public  void DestroyALL()
    {
        for(int i = listOfGameObj.Count -1 ;i >=0; i --)
        {
            GameObject temp = listOfGameObj[i];          
            listOfGameObj.RemoveAt(i);
            GameObject.Destroy(temp.gameObject);
        }


    }

    public void SaveAllInfoInBinary()
    {
        //List<GameObject> listOfGameObjToSave = listOfGameObj;
        


        for (int i = 0; i < listOfGameObj.Count; i++)
        {
            Info newInfo = new Info();

            GameObject a = listOfGameObj[i];
            Rigidbody rb = a.GetComponent<Rigidbody>();
            Material mat = a.GetComponent<Material>();
            newInfo.SetMyVelo(rb.velocity);
            newInfo.SetMyPos(a.transform.position);
            newInfo.SetMyAngularVelo(rb.angularVelocity);
            newInfo.SetMyRotation(a.transform.eulerAngles);
            newInfo.SetMyObjId(int.Parse(a.name));
            
           // newInfo.SetMyColour(new Vector3(mat.color.r, mat.color.g, mat.color.b));

            classToSerialize.myList.Add(newInfo);
           

        }

        string path = Application.dataPath + "/Resources/MyData.txt";
        
        BinaryDataSaving.SaveDataToDisk(path, classToSerialize);


    }




    public void LoadAllInfoFromBinary()
    {
        
        string path = Application.dataPath + "/Resources/MyData.txt";
        SerializableInfoClass serializableInfoClass = LoadBinaryDataFromDisk<SerializableInfoClass>(path);
        
        for (int i = 0; i < serializableInfoClass.myList.Count; i++)
        {

            VectorSerialized pos = serializableInfoClass.myList[i].GetMyPos();
            VectorSerialized rot = serializableInfoClass.myList[i].GetMyRotation();
            VectorSerialized velo = serializableInfoClass.myList[i].getMyVelo();
            VectorSerialized angularVelo = serializableInfoClass.myList[i].getMyAngularVelo();
            VectorSerialized colour = serializableInfoClass.myList[i].getMyColour();

            GameObject newObj = Instantiate(items[serializableInfoClass.myList[i].getObjId()]) as GameObject;

            Rigidbody rb = newObj.GetComponent<Rigidbody>();
            Material mat = newObj.GetComponent<Material>();
            /*Color col = new Color();
            col.r = colour.x;
            col.g = colour.y;
            col.b = colour.z;*/

            newObj.transform.position = new Vector3(pos.x, pos.y, pos.z);
            newObj.transform.eulerAngles = new Vector3(rot.x, rot.x, rot.z);
            rb.angularVelocity = new Vector3(angularVelo.x, angularVelo.y, angularVelo.z);
            rb.velocity = new Vector3(velo.x, velo.y, velo.z);

            // mat.color = col;

            listOfGameObj.Add(newObj);



        }

        

    }




    public void SaveAllInfoInXml()
    {
        //List<GameObject> listOfGameObjToSave = listOfGameObj;

      

        for (int i = 0; i < listOfGameObj.Count; i++)
        {
            Info newInfo = new Info();

            GameObject a = listOfGameObj[i];
            Rigidbody rb = a.GetComponent<Rigidbody>();
            Material mat = a.GetComponent<MeshRenderer>().material;
            newInfo.SetMyVelo(rb.velocity);
            newInfo.SetMyPos(a.transform.position);
            newInfo.SetMyAngularVelo(rb.angularVelocity);
            newInfo.SetMyRotation(a.transform.eulerAngles);
            newInfo.SetMyObjId(int.Parse(a.name));

             newInfo.SetMyColour(new Vector3(mat.color.r, mat.color.g, mat.color.b));

            classToSerialize.myList.Add(newInfo);


        }

        string path = Application.dataPath + "/Resources/MyXMLData.xml";

       

        XmlDataSaving.SaveDataToDisk(path, classToSerialize);


    }



    public void SaveAllInfoInJson()
    {
        //List<GameObject> listOfGameObjToSave = listOfGameObj;



        for (int i = 0; i < listOfGameObj.Count; i++)
        {
            Info newInfo = new Info();

            GameObject a = listOfGameObj[i];
            Rigidbody rb = a.GetComponent<Rigidbody>();
            Material mat = a.GetComponent<MeshRenderer>().material;
            newInfo.SetMyVelo(rb.velocity);
            newInfo.SetMyPos(a.transform.position);
            newInfo.SetMyAngularVelo(rb.angularVelocity);
            newInfo.SetMyRotation(a.transform.eulerAngles);
            newInfo.SetMyObjId(int.Parse(a.name));

            newInfo.SetMyColour(new Vector3(mat.color.r, mat.color.g, mat.color.b));

            classToSerialize.myList.Add(newInfo);


        }

        string path = Application.dataPath + "/Resources/MyJsonData.json";



        JsonDataSaving.SaveDataToDisk(path, classToSerialize);


    }



    public void LoadAllInfoFromXML()
    {
        
        string path = Application.dataPath + "/Resources/MyXMLData.xml";
        SerializableInfoClass serializableInfoClass = LoadXMLDataFromDisk<SerializableInfoClass>(path);

        for (int i = 0; i < serializableInfoClass.myList.Count; i++)
        {

            VectorSerialized pos = serializableInfoClass.myList[i].GetMyPos();
            VectorSerialized rot = serializableInfoClass.myList[i].GetMyRotation();
            VectorSerialized velo = serializableInfoClass.myList[i].getMyVelo();
            VectorSerialized angularVelo = serializableInfoClass.myList[i].getMyAngularVelo();
            VectorSerialized colour = serializableInfoClass.myList[i].getMyColour();

            GameObject newObj = Instantiate(items[serializableInfoClass.myList[i].getObjId()]) as GameObject;

            Rigidbody rb = newObj.GetComponent<Rigidbody>();
            Material mat = newObj.GetComponent<MeshRenderer>().material;
            Color col = new Color();
            col.r = colour.x;
            col.g = colour.y;
            col.b = colour.z;

            newObj.transform.position = new Vector3(pos.x, pos.y, pos.z);
            newObj.transform.eulerAngles = new Vector3(rot.x, rot.x, rot.z);
            rb.angularVelocity = new Vector3(angularVelo.x, angularVelo.y, angularVelo.z);
            rb.velocity = new Vector3(velo.x, velo.y, velo.z);

            mat.color = col;

            listOfGameObj.Add(newObj);



        }



    }



    //public void LoadAllInfoFromJson()
    //{

    //    string path = Application.dataPath + "/Resources/MyJsonData.json";
    //    SerializableInfoClass serializableInfoClass = LoadJsonDataFromDisk<SerializableInfoClass>(path);

    //    for (int i = 0; i < serializableInfoClass.myList.Count; i++)
    //    {

    //        VectorSerialized pos = serializableInfoClass.myList[i].GetMyPos();
    //        VectorSerialized rot = serializableInfoClass.myList[i].GetMyRotation();
    //        VectorSerialized velo = serializableInfoClass.myList[i].getMyVelo();
    //        VectorSerialized angularVelo = serializableInfoClass.myList[i].getMyAngularVelo();
    //        VectorSerialized colour = serializableInfoClass.myList[i].getMyColour();

    //        GameObject newObj = Instantiate(items[serializableInfoClass.myList[i].getObjId()]) as GameObject;

    //        Rigidbody rb = newObj.GetComponent<Rigidbody>();
    //        Material mat = newObj.GetComponent<MeshRenderer>().material;
    //        Color col = new Color();
    //        col.r = colour.x;
    //        col.g = colour.y;
    //        col.b = colour.z;

    //        newObj.transform.position = new Vector3(pos.x, pos.y, pos.z);
    //        newObj.transform.eulerAngles = new Vector3(rot.x, rot.x, rot.z);
    //        rb.angularVelocity = new Vector3(angularVelo.x, angularVelo.y, angularVelo.z);
    //        rb.velocity = new Vector3(velo.x, velo.y, velo.z);

    //        mat.color = col;

    //        listOfGameObj.Add(newObj);



    //    }



    //}




    public T LoadBinaryDataFromDisk<T>(string filePath)
    {
        T toRet;
        string path = filePath;
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


    public T LoadXMLDataFromDisk<T>(string filePath)
    {
        T toRet;
        string path =  filePath;
        if (File.Exists(path))
        {          
             var serializer = new XmlSerializer(typeof(SerializableInfoClass));           
            FileStream file = File.Open(path, FileMode.Open);
            toRet = (T)serializer.Deserialize(file);
            file.Close();
        }
        else
            toRet = default(T);
        return toRet;
    }

    //public T LoadJsonDataFromDisk<T>(string filePath)
    //{
    //    T toRet;
    //    string path = filePath;
    //    if (File.Exists(path))
    //    {



    //        string jsonString = File.ReadAllText(path);

    //        //FileStream file = File.Open(path, FileMode.Open);
    //        toRet = (T)jsonString;
    //        //file.Close();

    //    }
    //    else
    //        toRet = default(T);
    //    return toRet;
    //}



    public void LoadFromJson()
    {
        string jsonString;
        string path = Application.dataPath + "/Resources/MyJsonData.json";
        jsonString = File.ReadAllText(path);
        SerializableInfoClass serializableInfoClass = (SerializableInfoClass)JsonUtility.FromJson(jsonString, typeof(SerializableInfoClass));



        for (int i = 0; i < serializableInfoClass.myList.Count; i++)
        {

            VectorSerialized pos = serializableInfoClass.myList[i].GetMyPos();
                VectorSerialized rot = serializableInfoClass.myList[i].GetMyRotation();
            VectorSerialized velo = serializableInfoClass.myList[i].getMyVelo();
            VectorSerialized angularVelo = serializableInfoClass.myList[i].getMyAngularVelo();
            VectorSerialized colour = serializableInfoClass.myList[i].getMyColour();

            GameObject newObj = Instantiate(items[serializableInfoClass.myList[i].getObjId()]) as GameObject;

            Rigidbody rb = newObj.GetComponent<Rigidbody>();
            Material mat = newObj.GetComponent<MeshRenderer>().material;
            Color col = new Color();
            col.r = colour.x;
            col.g = colour.y;
            col.b = colour.z;

            newObj.transform.position = new Vector3(pos.x, pos.y, pos.z);
            newObj.transform.eulerAngles = new Vector3(rot.x, rot.x, rot.z);
            rb.angularVelocity = new Vector3(angularVelo.x, angularVelo.y, angularVelo.z);
            rb.velocity = new Vector3(velo.x, velo.y, velo.z);

            mat.color = col;

            listOfGameObj.Add(newObj);



        }




        //}


    }






    public static class XmlDataSaving
    {
        
        public static void SaveDataToDisk(string filePath, object toSave)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(toSave.GetType());

            string path = Path.Combine(Application.streamingAssetsPath, filePath);
            FileStream file = File.Create(path);
            
            x.Serialize(file, toSave);

            file.Close();


        }
    }


    public static class JsonDataSaving
    {

        public static void SaveDataToDisk(string filePath, object toSave)
        {
           // string data = JsonUtility.ToJson(toSave);

            string jsonData = JsonUtility.ToJson(toSave, true);

            //string path = Application.streamingAssetsPath + "/" + filePath ;

            string path = Path.Combine(Application.streamingAssetsPath, filePath);

            //FileStream file = File.Create(path);

            File.WriteAllText(path , jsonData);

            //file.Close();


        }
    }






    static class BinaryDataSaving
    {
        public static void SaveDataToDisk(string filePath, object toSave)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Path.Combine(Application.streamingAssetsPath, filePath);
            FileStream file = File.Create(path);
            bf.Serialize(file, toSave);
            file.Close();
        }
    }
}

[System.Serializable]
public class SerializableInfoClass
{
    public List<Info> myList;


    public SerializableInfoClass()
    {
        myList = new List<Info>();
    }
}


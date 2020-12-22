using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button resetButton;
    public Button saveBinary;
    public MainFlow mf;
    void Start()
    {
        
        
    }



    public void TaskOnResetButtonClick()
    {
        mf.DestroyALL();
        mf.SpwanRendomObjects();

    }


    public void TaskOnBinarySaveButtoClick()
    {
        mf.SaveAllInfoInBinary();

    }

    public void TaskOnBinaryLoadButtoClick()
    {
        mf.DestroyALL();
        mf.LoadAllInfoFromBinary();

    }

    public void XMLSaveButtonClick()
    {
        mf.SaveAllInfoInXml();


    }

    public void XMLLoadButtonClick()
    {
        mf.DestroyALL();
        mf.LoadAllInfoFromXML();

    }

    public void TaskOnJsonSaveButtonClick()
    {
        mf.SaveAllInfoInJson();

    }

    public void TaskOnJsonLoadButtonClick()
    {
        mf.DestroyALL();
        mf.LoadFromJson();

    }




}

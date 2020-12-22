using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Info
{
    public VectorSerialized myVelo;
    public VectorSerialized myAngularVelo;
    public VectorSerialized mypos;
    public VectorSerialized myRotation;
    public int objId;
    public VectorSerialized myColour;




    public void SetMyObjId(int id)
    {
       this.objId = id;

    }

    public int getObjId()
    {
        return objId;
    }

    public void SetMyColour( Vector3 colourToPass)
    {
        myColour.x = colourToPass.x;
        myColour.y = colourToPass.y;
        myColour.z = colourToPass.z;

    }

    public VectorSerialized getMyColour()
    {
        return myColour;
    }

    public void SetMyVelo(Vector3 veloToPass)
    {
        myVelo.x = veloToPass.x;
        myVelo.y = veloToPass.y;
        myVelo.z = veloToPass.z;
    }

    public VectorSerialized getMyVelo()
    {
        return myVelo;
    }

    public void SetMyAngularVelo(Vector3 angularVeloToPass)
    {
        myAngularVelo.x = angularVeloToPass.x;
        myAngularVelo.y = angularVeloToPass.y;
        myAngularVelo.z = angularVeloToPass.z;
    }

    public VectorSerialized getMyAngularVelo()
    {
        return myAngularVelo;
    }

    public VectorSerialized GetMyPos()
    {
        return mypos;
    }


    public VectorSerialized GetMyRotation()
    {
        return myRotation;
    }

    public void SetMyRotation ( Vector3 rotation)
    {

        myRotation.x = rotation.x;
        myRotation.y = rotation.y;
        myRotation.z = rotation.z;
    }

    public void SetMyPos(Vector3 pos)
    {
        mypos.x = pos.x;
        mypos.y = pos.y;
        mypos.z = pos.z;

    }

    
}
[System.Serializable]
public struct VectorSerialized
{
    public float x;
    public float y;
    public float z;

    public VectorSerialized(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Pet
{
    public override void PetThePet()
    {
        base.PetThePet();
        Debug.Log("Cat love to hunt");
    }
}

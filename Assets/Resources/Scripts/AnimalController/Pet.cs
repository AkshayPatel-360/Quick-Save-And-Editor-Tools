using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    // Start is called before the first frame update
    new string name;
    void Awake()
    {
        
        name = this.GetType().ToString();
        Debug.Log("Oh its : " + name);
       // PetThePet();
    }

    public  virtual  void PetThePet()
    {
        Debug.Log("You pet the : " + name);
    }
    void Update()
    {
        
    }

    //// Start is called before the first frame update
    //public string nameToPet;

    //void Start()
    //{


    //    try
    //    {
    //        GameObject gameObject = new GameObject();
    //        System.Type typeofAnimal = System.Type.GetType(nameToPet);
    //        Pet pet = (Pet)gameObject.AddComponent(typeofAnimal);
    //        pet.PetThePet();
    //    }
    //    catch (System.Exception e)
    //    {

    //        Debug.Log("Wrong Pet");
    //        Destroy(gameObject);
    //    }

    // }




}

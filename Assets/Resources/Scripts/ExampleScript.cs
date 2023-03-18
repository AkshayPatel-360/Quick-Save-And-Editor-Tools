using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Reflection;
using System.IO;

public class ExampleScript : EditorWindow
{

    [MenuItem("Custom Tools/Create Prefabs")]
    public static void ShowCasePRefab()
    {
        EditorWindow window = GetWindow<ExampleScript>("Create Prefabs");
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.BeginArea(new Rect(10, 100, 300, 100));
        //  string[] petNames = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/Scripts/Animal/","*.cs");
       
       
        if (GUILayout.Button("Press Me To Create Prefabs"))
        {
            object[] gameobjects = Resources.LoadAll("AnimalPrefabs", typeof(GameObject));
            string[] petPrefabPaths = new string[gameobjects.Length];
          
            Debug.Log("Number Of Prefabs "+gameobjects.Length);

            if (gameobjects.Length == 0)
            {
                CreatePrefab();
            }
            else
            {
                for (int i = 0; i < gameobjects.Length; i++)
                {
                    petPrefabPaths[i] = AssetDatabase.GetAssetPath((UnityEngine.Object)gameobjects[i]);
                    Debug.Log(petPrefabPaths[i]);
                    File.Delete(petPrefabPaths[i]);
                }
               
            }
           
          

        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        GUILayout.EndHorizontal();
    }

    private void CreatePrefab()
    {
        string[] petNames = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/Scripts/Animal/", "*.cs");
        object[] animalSprites = Resources.LoadAll("AnimalSprite", typeof(Sprite));
        Sprite[] sprites = new Sprite[animalSprites.Length];
        for (int i = 0; i < animalSprites.Length; i++)
        {
            sprites[i] = (Sprite)animalSprites[i];
        }
        // Debug.Log("Animal Sprite Length: "+animalSprites.Length);
        for (int i = 0; i < petNames.Length; i++)
        {
            petNames[i] = System.IO.Path.GetFileNameWithoutExtension(petNames[i]);
        }

        for (int i = 0; i < petNames.Length; i++)
        {

            for (int j = 0; j < sprites.Length; j++)
            {
                if (petNames[i].Equals(sprites[j].name))
                {
                    CreatePet(petNames[i], sprites[j]);
                }
            }

        }
    }

    public void CreatePet(String petName,Sprite petSprite)
    {
        try
        {
            GameObject petobject = new GameObject();
            petobject.name = petName;
            System.Type typeofAnimal = System.Type.GetType(petName);
            SpriteRenderer sr =  petobject.AddComponent<SpriteRenderer>();
            sr.sprite = petSprite;
            Pet pet = (Pet)petobject.AddComponent(typeofAnimal);
        
            try
            {
                string localPath = Application.dataPath + "/Resources/AnimalPrefabs/" + petobject.name + ".prefab";
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
                PrefabUtility.SaveAsPrefabAssetAndConnect(petobject, localPath, InteractionMode.UserAction);
            }
            catch (Exception)
            {

                Debug.Log("Prefab is Not Created : "+petobject.name);
            }
            DestroyImmediate(petobject);           
        }
        catch (System.Exception e)
        {

        }
    }
    //private Sprite GetAnimalSprite(string animal)
    //{
    //    System.Type type = GetType();
    //    FieldInfo info = type.GetField(animal);
    //    if (info == null)
    //    {
    //        Debug.Log("There is no animalSprite : "+animal);
    //    }
    //    return (Sprite)info.GetValue(this);
    //}


}
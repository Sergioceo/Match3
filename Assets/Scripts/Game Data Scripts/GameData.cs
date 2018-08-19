using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] highScores;
    public int[] stars;
}


public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;

    private void Awake()
    {
        if(gameData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
        } else
        {
            Destroy(this.gameObject);
        }
        Load();
    }

    private void Start()
    {
        
    }


    public void Save()
    {
        //Create Binary Formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();
        
        //Create a route from the program to the file
        FileStream file = File.Open(Application.persistentDataPath + "/Player.dat", FileMode.Create);

        //Create a copy of save data
        SaveData data = new SaveData();
        data = saveData;
        
        //Now to actually save
        formatter.Serialize(file, data);
        
        //Close the data stream
        file.Close();

        Debug.Log("Saved");


    }

    public void Load()
    {
        //Check if the save game file exists
        if(File.Exists(Application.persistentDataPath + "/Player.dat"))
        {
            // Create Binary Formatter which can read binary files
            BinaryFormatter formatter = new BinaryFormatter();
            //Create a route from the program to the file
            FileStream file = File.Open(Application.persistentDataPath + "/Player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Player Data Loaded");

        } else
        {
            saveData = new SaveData();
            saveData.isActive = new bool[9];
            saveData.stars = new int[9];
            saveData.highScores = new int[9];
            saveData.isActive[0] = true;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }
}

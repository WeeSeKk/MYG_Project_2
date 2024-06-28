using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Data data = new Data(); 

    public void SaveToJson()//Create a json file in the persistent data folder
    {
        string datastring = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/User_Data.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, datastring);
    }

    public void LoadFromJson()//Read the Json file created on the computer
    {
        string filePath = Application.persistentDataPath + "/User_Data.json";
        string datastring = System.IO.File.ReadAllText(filePath);

        data = JsonUtility.FromJson<Data>(datastring);
    }

    void Start()//Load the json file for registered users
    {
        LoadFromJson();
    }
}

[System.Serializable]
public class Data//List of user 
{
    public List<LoginInfos> datas = new List<LoginInfos>();
}    

[System.Serializable]
public class LoginInfos//Email and password associated to a user
{
    public string emails;
    public string passwords;
}
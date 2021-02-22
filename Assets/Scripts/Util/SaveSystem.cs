using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveUser(User user)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/user.did";
        FileStream stream = new FileStream(path, FileMode.Create);
        UserData data = new UserData(user);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved at : " + path);
    }

    public static UserData LoadUser()
    {
        string path = Application.persistentDataPath + "/user.did";
        Debug.Log("Loading from : " + path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserData data = formatter.Deserialize(stream) as UserData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
        
    }
}

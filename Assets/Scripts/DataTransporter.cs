using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataTransporter : MonoBehaviour
{
    public static DataTransporter transporterInstance;
    public string playerName;

    public int highScore = 0;
    public string topPlayerName;

    private void Awake()
    {
        // Here is the Singltone pattern
        if (transporterInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        transporterInstance = this;
        DontDestroyOnLoad(gameObject);

        //LoadColor(); - here will be a method which loads the best score
    }

    private void Start()
    {
        LoadHighScore();
    }

    public void SetNameForThisSession(string name)
    {
        playerName = name;
        //Debug.Log("Name for this session " + playerName);
    }

    public string GetNameForThisSession()
    {
        return playerName;
    }

    public void CompareScore(int currentSessionScore, string currentSessionPlayerName)
    {
        if (currentSessionScore > highScore)
        {
            highScore = currentSessionScore;
            topPlayerName = currentSessionPlayerName;
            SaveWriteHighScore();
        }
        else
        {
            return;
        }
    }



    [System.Serializable]
    class SaveScoreAndName
    {
        public int highScore;
        public string topPlayerName;
    }




    public void SaveWriteHighScore()
    {
        SaveScoreAndName data = new SaveScoreAndName();
        data.highScore = highScore;
        data.topPlayerName = topPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }



    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveScoreAndName data = JsonUtility.FromJson<SaveScoreAndName>(json);

            highScore = data.highScore;
            topPlayerName = data.topPlayerName;
        }
    }


    public string GetTopPlayerName()
    {
        return topPlayerName;
    }

    public int GetTopPlayerScore()
    {
        return highScore;
    }
}

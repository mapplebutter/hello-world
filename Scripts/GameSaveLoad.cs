using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveLoad : MonoBehaviour
{

    /// <summary>
    /// Modifies a SaveData Object to load data to and from a file. The SaveData class is found in GameManager.cs
    /// </summary>
    static SaveData data = new SaveData();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete)) DeleteSaveData();
    }

    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameDataSave.dat");

        //Save player and score
        data.playerList.Add(new PlayerData(GameManager.instance.playerName, GameManager.instance.timeTaken, ScoreAssessment.instance.GetTotalScore()));

        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + "/GameDataSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameDataSave.dat", FileMode.Open);
            data = (SaveData)bf.Deserialize(file);

            foreach (PlayerData player in data.playerList)
            {
                GameManager.instance.highscoreNames.Add(player.playerName);
                GameManager.instance.highscoreTimes.Add(player.playerTime);
                GameManager.instance.highscoreScores.Add(player.totalScore);
            }
            file.Close();

        }
    }

    public static void DeleteSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/GameDataSave.dat"))
        {
            File.Delete(Application.persistentDataPath + "/GameDataSave.dat");
        }
    }

    public static List<string> GetNames()
    {
        List<string> playerNames = new List<string>();
        if (File.Exists(Application.persistentDataPath + "/GameDataSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameDataSave.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);

            foreach (PlayerData player in data.playerList)
            {
                playerNames.Add(player.playerName);
            }
        }
        return playerNames;
    }

    public static string UseSameLastName()
    {
        if (File.Exists(Application.persistentDataPath + "/GameDataSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameDataSave.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);

            if (data.playerList[data.playerList.Count - 1].playerName.Length != 0) return data.playerList[data.playerList.Count - 1].playerName;
            else return "Steve";
        }

        return "Steve";
    }

    [System.Serializable]
    public class SaveData
    {
        public List<PlayerData> playerList;


        public SaveData()
        {
            playerList = new List<PlayerData>();
        }
    }

    [System.Serializable]
    public struct PlayerData
    {
        public string playerName;
        public float playerTime;

        public int totalScore;

        public PlayerData(string name, float time, int score)
        {
            this.playerName = name;
            this.playerTime = time;
            this.totalScore = score;
        }
    }
}

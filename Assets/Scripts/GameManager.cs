using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Intermediate class holding savedata
    [Serializable]
    private class SaveData
    {
        public int highscore;
        public string highscorePlayerName;
    }

    // Singleton instance set on Awake
    public static GameManager instance { get; private set; }

    public int scoreCurrent { get; private set; } = 0;
    public int scoreHigh { get; private set; } = 0;
    public string playerNameCurrent { get; set; } = "None";
    public string playerNameHigh { get; set; } = "None";

    // Save to file
    private void Save()
    {
        // Prepare saveData
        SaveData saveData = new SaveData();
        saveData.highscore = scoreHigh;
        saveData.highscorePlayerName = playerNameHigh;

        // Convert to json string
        string jsonSaveData = JsonUtility.ToJson(saveData);

        // Write to savedata.json in persistent data path
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", jsonSaveData);
    }

    // Load from savedata file. Do nothing if savedata file does not exist
    private void Load()
    {
        // Construct file path (savedata.json from persitent data path)
        string saveDataFilePath = Application.persistentDataPath + "/savedata.json";

        // Check file existence, abort loading if file does not exist
        if (!File.Exists(saveDataFilePath)) return;

        // Load savedata.json from persistent data path
        string jsonSaveData = File.ReadAllText(saveDataFilePath);

        // Convert json string to SaveData
        SaveData saveData = JsonUtility.FromJson<SaveData>(jsonSaveData);

        // Store loaded data to class variable
        scoreHigh = saveData.highscore;
        playerNameHigh = saveData.highscorePlayerName;
    }

    private void Awake()
    {
        // Don't set new instance if instance already existed (have been Awake before)
        if (instance) { Destroy(gameObject); return; }

        // Set singleton instance
        instance = this;
        Debug.Log("Set GameManager instance");

        // Inform Unity to not destroy this GameObject when loading a new scene
        DontDestroyOnLoad(gameObject);

        // Load savedata
        Load();
    }

    // Load main scene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Update current score, also update high score if current score is not less than it
    public void UpdateScore(int score)
    {
        // Update scoreCurrent
        scoreCurrent = score;

        // Update playerNameHigh and scoreHigh if scoreCurrent is >=
        if (scoreCurrent >= scoreHigh)
        {
            playerNameHigh = playerNameCurrent;
            scoreHigh = scoreCurrent;
        }
    }

    // Save high score and load menu scene
    public void BackToMenu()
    {
        Save();
        SceneManager.LoadScene(0);
    }

    // Quit game
    public void ExitGame()
    {
        // Quit game
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}

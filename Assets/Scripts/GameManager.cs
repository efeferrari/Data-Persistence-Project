using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string username;
    public int currentScore = 0;
    public bool isGameOver = false;

    public int bestScore;
    public string bestPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Set Best Scores
        LoadData();
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void StartMainScene()
    {
        StartScene(1);
    }

    public void StartScene(int scene = 0)
    {
        SceneManager.LoadScene(scene);
    }

    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
# endif
    }

    [System.Serializable]
    class SavedData
    {
        public string bestPlayer;
        public int bestScore;
    }

    public void SaveData()
    {
        SavedData data = GetSavedData();

        if (currentScore > data.bestScore)
        {
            bestPlayer = username;
            bestScore = currentScore;

            data.bestPlayer = username;
            data.bestScore = currentScore;

            string jsonData = JsonUtility.ToJson(data);
            string path = Application.persistentDataPath + "/saveFile.json";

            File.WriteAllText(path, jsonData);
        }
    }

    public void LoadData()
    {
        SavedData data = GetSavedData();

        bestPlayer = data.bestPlayer;
        bestScore = data.bestScore;
    }

    private SavedData GetSavedData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            return JsonUtility.FromJson<SavedData>(json);
        }

        return new SavedData();
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
    using UnityEditor;
#endif


public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;

    [SerializeField] private MenuUIManager menuUIManager;

    [SerializeField] public string gUserName;

    private int currentScene;


    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        GameObject menuUIManagerRef = GameObject.Find("MenuUIManager");
        menuUIManager = menuUIManagerRef.GetComponent<MenuUIManager>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "menu"){
            gUserName = menuUIManager.userName;             // do not try to assign name unless on starting screen
        }
        
    }


    [System.Serializable]
    class SaveData
    {
        public string userName;
        public int score;
    }


    public void SaveDataCall(string username, int score)
    {
        string filePath = $"D:/DataFolder/{username}.json";

        SaveData data = new SaveData
        {
            userName = username,
            score = score
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Data saved: " + username + ", " + score + ".");
    }


    public int LoadDataCall(string username)
    {
        string filePath = $"D:/DataFolder/{username}.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.score;
        }
        else
        {
            Debug.LogWarning($"File not found for user: {username}");
            return 0;
        }
    }

    public int GetHighestScore()
    {
        string folderPath = "D:/DataFolder";
        int highestScore = 0;

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");

            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                if (data.score > highestScore)
                {
                    highestScore = data.score;
                }
            }
        }
        else
        {
            Debug.LogWarning("Data folder does not exist.");
        }

        return highestScore;
    }



}

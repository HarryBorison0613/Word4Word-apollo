using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalServer : MonoBehaviour
{
    static public LocalServer ls;
    private string savePath;

    public void Start()
    {
        if (ls == null) ls = this;
        #if UNITY_ANDROID
            savePath = "/storage/emulated/0/Documents/"+Application.identifier.ToString()+"/wordData.txt";
        #elif UNITY_STANDALONE_WIN 
            savePath = Application.persistentDataPath + "/wordData.txt";
        #endif
        
    }

    public void FetchWords() {
        ReadString();
    }
    public static void ReadString()
    {
        TextAsset wordText = Resources.Load<TextAsset>("wordList");
        string path = wordText.text;

        string[] fileLine = path.Split('\n');
        List<int> randomList = new List<int>();
        GamePlay.play.words.Clear();
        for (int cnt = 0; cnt < 4; cnt++) {
            int numToAdd = Random.Range(0, fileLine.Length);
            while (randomList.Contains(numToAdd)) {
                numToAdd = Random.Range(0, 4);
            }
            GamePlay.play.words.Add(fileLine[numToAdd].ToUpper().Substring(0, 4));
            Debug.Log(GamePlay.play.words[cnt]);
        }
    }

    public void Auth(string name, string email, string password, string flag) {
        if (flag == "REGISTER") {
            string uEmail = PlayerPrefs.GetString("email");
            if (uEmail == email) {
                UI_Auth.UIA.SetErrAlertText("User Already Exist.");
            } else {
                PlayerPrefs.SetString("name", name);
                PlayerPrefs.SetString("email", email);
                PlayerPrefs.SetString("pwd", password);
                UI_Main.UIM.authPage.gameObject.SetActive(false);
                UI_Main.UIM.menuPage.gameObject.SetActive(true);
            }
        } else {
            string uEmail = PlayerPrefs.GetString("email");
            string pwd = PlayerPrefs.GetString("pwd");
            if (uEmail == email && pwd == password) {
                UI_Main.UIM.authPage.gameObject.SetActive(false);
                UI_Main.UIM.menuPage.gameObject.SetActive(true);
            } else {
                UI_Auth.UIA.SetErrAlertText("User Information Failed. Please try again.");
            }
        }
    }

    public Save CreateSaveData(List<string> words, List<char> letters, List<string> clue)
    {
        Save save = new Save();
        save.wordList = words;
        save.letters = letters;
        save.clue = clue;
        save.score = GamePlay.play.score;
        save.clickNumber = GamePlay.play.clicker;
        save.timeRemaining = GamePlay.play.timeRemaining;
        save.lossNum = GamePlay.play.lossNum;
        return save;
    }
    public void SaveData(List<string> words, List<char> letters, List<string> clue) {
        Save save = CreateSaveData(words, letters, clue);
        string json = JsonUtility.ToJson(save);
        FileInfo file = new FileInfo(savePath);
        if (!file.Exists)
        {
            StreamWriter w = file.CreateText();
            w.WriteLine(json);
            w.Close();
        }
        else
        {
            StreamWriter w = file.AppendText();
            w.WriteLine(json);
            w.Close();
        }
    }
    public List<Save> LoadData()
    {
        if (File.Exists(savePath))
        {
            StreamReader data = new StreamReader(savePath);
            Debug.Log(data);
            string[] jsons = data.ReadToEnd().Split('\n');
            List<Save> gameDatas = new List<Save>();
            gameDatas.Clear();
            for (int cnt = 0; cnt < jsons.Length - 1; cnt++)
            {
                Save save = JsonUtility.FromJson<Save>(jsons[cnt]);
                gameDatas.Add(save);
            }
            data.Close();
            return gameDatas;
        } else
        {
            FileInfo file = new FileInfo(savePath);
            StreamWriter w = file.CreateText();
            w.Close();
            List<Save> gameDatas = new List<Save>();
            Save gameData = new Save();
            gameData.wordList.Add("Nothing");
            gameDatas.Add(gameData);
            return gameDatas;
        }
    }

    [System.Serializable]
    public class Save {
        public List<string> wordList = new List<string>();
        public List<char> letters = new List<char>();
        public List<string> clue = new List<string>();
        public int score;
        public int clickNumber;
        public float timeRemaining;
        public int lossNum;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameAPI : MonoBehaviour
{
    static public GameAPI request;
    public string _base = string.Empty;
    public string callbackResult = string.Empty;
    public string auth_status = string.Empty;
    public bool userLoginStatus = false;

    void Start()
    {
        if(request == null) request = this;
        _base = gameProperty.SERVER_URL;
    }

    public void setHeader(UnityWebRequest uwrq)
    {
        uwrq.SetRequestHeader("Accept", "*/*");
        uwrq.SetRequestHeader("Accept-Encoding", "gzip, deflate");
        uwrq.SetRequestHeader("User-Agent", "runscope/0.1");
    }

    public void FetchWords() {
        string URL = _base + "play/words";
        // UI_Main.UIM.activityBar.gameObject.SetActive(true);
        Debug.Log("url"+URL);

        UnityWebRequest uwrq = UnityWebRequest.Get(URL);
        setHeader(uwrq);
        uwrq.SetRequestHeader("Content-Type", "application/json");
        //Test
        StartCoroutine(WaitForRequest(uwrq, "FETCH_WORDS"));
    }
    public void LogIn(string name, string email, string password, string flag) {
        string URL = _base + "auth/auth";
        WWWForm form = new WWWForm();

        if(flag == "REGISTER") {
            form.AddField("name", name);
            form.AddField("email", email);
            form.AddField("password", password);
        }
        else {
            form.AddField("email", email);
            form.AddField("password", password);
        }
        form.AddField("flag", flag);
        UnityWebRequest uwrq = UnityWebRequest.Post(URL, form);
        setHeader(uwrq);
        auth_status = flag;
        // UI_Main.UIM.loading.gameObject.SetActive(true);
        StartCoroutine(WaitForRequest(uwrq, "AUTH"));
    }

    IEnumerator WaitForRequest(UnityWebRequest uwrq, string flag)
    {
        uwrq.timeout = 30;
        yield return uwrq.SendWebRequest();
        // UI_Main.UIM.activityBar.gameObject.SetActive(false);
        callbackResult = uwrq.downloadHandler.text;
        if (uwrq.result == UnityWebRequest.Result.ConnectionError)
        {
            UI_Main.UIM.netErrorAlert.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = gameProperty.NETWORK_ERROR_TITLE;
            UI_Main.UIM.netErrorAlert.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = gameProperty.NETWROK_ERROR_CONTENT;
            UI_Main.UIM.netErrorAlert.gameObject.SetActive(true);
            //Test
            Debug.Log("Net Errors");
        }
        else
        {
            if( uwrq.isDone) {
                switch (flag)
                {
                    case "FETCH_WORDS":
                        WordGroup res = JsonUtility.FromJson<WordGroup>(callbackResult);
                        if(res.status) {
                            GamePlay.play.words = res.result;
                        }
                        break;
                    case "AUTH":
                        Auth userAuthInfo = JsonUtility.FromJson<Auth>(callbackResult);
                        userLoginStatus = userAuthInfo.status;
                        if (userLoginStatus)
                        {
                            UI_Main.UIM.playerId = userAuthInfo.result.user_id;
                            UI_Main.UIM.playerName = userAuthInfo.result.name;
                            UI_Main.UIM.playerEmail = userAuthInfo.result.email;
                            try
                            {
                                if (string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerID")))
                                {
                                    PlayerPrefs.SetString("PlayerID", userAuthInfo.result.user_id);
                                    PlayerPrefs.SetString("PlayerName", userAuthInfo.result.name);
                                    PlayerPrefs.SetString("PlayerEmail", userAuthInfo.result.email);
                                    PlayerPrefs.Save();
                                }
                            }
                            catch (SystemException err)
                            {
                                Debug.Log("GOT :" + err);
                            }

                            if (auth_status == "REGISTER")
                            {
                                UI_Auth.UIA.ToRegister();
                                UI_Main.UIM.authPage.gameObject.SetActive(false);
                                UI_Main.UIM.menuPage.gameObject.SetActive(true);
                                // UI_Auth.UIA.ToHowToPlay(); //have to change with above
                            }
                            else if (auth_status == "LOGIN")
                            {
                                UI_Auth.UIA.ToRegister();
                                UI_Main.UIM.authPage.gameObject.SetActive(false);
                                UI_Main.UIM.menuPage.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            Debug.Log("Error happend!");
                        }
                        break;
                }
            } else {
                 UI_Main.UIM.netErrorAlert.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameProperty.NETWORK_ERROR_TITLE;
                 UI_Main.UIM.netErrorAlert.GetChild(0).GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = gameProperty.NETWROK_ERROR_CONTENT;
                 UI_Main.UIM.netErrorAlert.gameObject.SetActive(true);

                Debug.Log("Net Error");
            }
        }
    }

    [Serializable]
    public class WordGroup {
        public bool status;
        public List<string> result;
    }

    [Serializable]
    public class Auth
    {
        public Userinfo result;
        public bool status;
        public string message;
        public int level;
    }

    [Serializable]
    public class Userinfo
    {
        public string user_id;
        public string name;
        public string email;
        public string avatar;
        public string score;
        public string country;
        public string reg_date;
        public string token;
        public string coin;
        public string point;
        public string socialId;
        public string socialType;
    }
}

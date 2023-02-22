using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    static public UI_Main UIM;
    public Transform startPage;
    public Transform authPage;
    public Transform menuPage;
    public Transform gamePlayPage;
    public Transform netErrorAlert;

    public Button startBtn;
    private Button backBtn;
    private Button loginBtn;
    private Button toRegisterBtn;
    private Button registerBtn;
    private Button singleBtn;
    private Button multiBtn;
    public Button exitBtn;

    public string playerId;
    public string playerName;
    public string playerEmail;

    public AudioSource click;
    

    void Start()
    {
        if(UIM == null) UIM = this;
        Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
        click = GetComponent<AudioSource>();

        if(!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerId"))) {
            playerEmail = PlayerPrefs.GetString("email");
        } else {
            playerEmail = string.Empty;
        }
        startBtn.onClick.AddListener(StartGame);
        exitBtn.onClick.AddListener(ExitGame);
    }
    void Update() {
        Positioning();
    }
    public void StartGame() {
        if(!string.IsNullOrEmpty(playerEmail)){
            startPage.gameObject.SetActive(false);
            authPage.gameObject.SetActive(false);
            menuPage.gameObject.SetActive(true);
        } else {
            startPage.gameObject.SetActive(false);
            authPage.gameObject.SetActive(true);
            menuPage.gameObject.SetActive(false);
        }
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void OnClickBackBtn(int flag) {
        switch(flag){
            case 1:
                authPage.gameObject.SetActive(false);
                startPage.gameObject.SetActive(true);
                InitializeInputField(0,2,1,2);
                break;
            case 2:
                authPage.GetChild(1).gameObject.SetActive(false);
                authPage.GetChild(0).gameObject.SetActive(true);
                InitializeInputField(1,-1,-1,4);
                break;
            case 3:
                menuPage.gameObject.SetActive(false);
                authPage.GetChild(1).gameObject.SetActive(false);
                authPage.GetChild(2).gameObject.SetActive(false);
                authPage.GetChild(0).gameObject.SetActive(true);
                authPage.gameObject.SetActive(true);
                break;
            case 4:
                gamePlayPage.gameObject.SetActive(false);
                gamePlayPage.GetChild(0).GetChild(2).GetChild(5).gameObject.SetActive(false);
                gamePlayPage.GetChild(0).GetChild(2).GetChild(6).gameObject.SetActive(false);
                gamePlayPage.GetChild(0).GetChild(2).GetChild(7).gameObject.SetActive(false);
                gamePlayPage.GetChild(0).GetChild(2).GetChild(8).gameObject.SetActive(false);
                gamePlayPage.GetChild(0).GetChild(5).GetChild(4).gameObject.SetActive(false);
                menuPage.gameObject.SetActive(true);
                break;
        }
    }
    public void InitializeInputField(int pos, int sub1, int sub2, int size) {
        if(sub1<0) {
            for (int i = 0; i < size; i++){
                authPage.GetChild(pos).GetChild(2).GetChild(i).GetComponent<TMP_InputField>().text = string.Empty;
            }
        } else {
            for (int i = 0; i < size; i++){
                authPage.GetChild(pos).GetChild(sub1).GetChild(sub2).GetChild(i).GetComponent<TMP_InputField>().text = string.Empty;
            }
        }
    }
    public void Positioning(){
        if(Screen.currentResolution.width > Screen.currentResolution.height) {
            float width =Screen.width;
            float height = Screen.height;
            startPage.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            startPage.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(644, 125);
            startPage.GetChild(1).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width/2, height/5);
            startPage.GetChild(1).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width/20, 18*height/20);
            startPage.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 64;

            authPage.GetChild(0).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            authPage.GetChild(1).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            authPage.GetChild(3).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            authPage.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            authPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(width/3, 100);
            authPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width/4, 3*height/5);
            authPage.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 50;
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 50;
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 36;
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = 36;
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.65f, height*0.9f);
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.65f+300, height*0.8f);
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.65f+250, height*0.1f);
            authPage.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.65f+100, height*0.65f);
            authPage.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.65f+100, height*0.45f);
            authPage.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.65f+100, height*0.25f);
            authPage.GetChild(0).GetChild(2).GetChild(2).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.65f+350, height*0.1f);
            authPage.GetChild(0).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.05f, height*0.85f);

            authPage.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            authPage.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            authPage.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 50;
            authPage.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 36;
            authPage.GetChild(1).GetChild(1).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.9f);
            authPage.GetChild(1).GetChild(1).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.85f);
            authPage.GetChild(1).GetChild(2).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.25f, height*0.65f);
            authPage.GetChild(1).GetChild(2).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.75f, height*0.65f);
            authPage.GetChild(1).GetChild(2).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.25f, height*0.4f);
            authPage.GetChild(1).GetChild(2).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.75f, height*0.4f);
            authPage.GetChild(1).GetChild(3).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.15f);
            authPage.GetChild(1).GetChild(3).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.05f, height*0.85f);

            authPage.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width*1.5f, height*1.5f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width*0.6f, height*0.8f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(width*0.5f, height*0.5f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().fontSize = 50;
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().fontSize = 50;
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.65f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.35f, height*0.7f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.7f, height*0.7f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.45f);
            menuPage.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            menuPage.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            menuPage.GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.3f, height*0.5f);
            menuPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.7f, height*0.5f);
            menuPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.95f, height*0.9f);
            menuPage.GetChild(0).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.63f, height*0.9f);
            menuPage.GetChild(0).GetChild(4).GetComponent<RectTransform>().transform.position = new Vector2(width*0.83f, height*0.9f);
            menuPage.GetChild(0).GetChild(5).GetComponent<RectTransform>().transform.position = new Vector2(width*0.9f, height*0.2f);
            menuPage.GetChild(0).GetChild(6).GetComponent<RectTransform>().transform.position = new Vector2(width*0.05f, height*0.9f);

            gamePlayPage.GetChild(0).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            gamePlayPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().localScale = new Vector2(3, 3);
            gamePlayPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -height * 0.13f);
            gamePlayPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().localScale = new Vector2(2.5f, 2.5f);
            gamePlayPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(width*0.3f, -height*0.3f);
            gamePlayPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            gamePlayPage.GetChild(0).GetChild(5).GetComponent<RectTransform>().localScale = new Vector2(3f, 3f);
            gamePlayPage.GetChild(0).GetChild(5).GetComponent<RectTransform>().transform.position = new Vector2(width*0.7f, height*0.55f);
            gamePlayPage.GetChild(0).GetChild(4).GetComponent<RectTransform>().anchoredPosition = new Vector2(width*0.05f, -height*0.1f);
            gamePlayPage.GetChild(0).GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(-width*0.2f, -height*0.3f);
            gamePlayPage.GetChild(0).GetChild(4).GetComponent<RectTransform>().anchoredPosition = new Vector2(-width*0.4f, -height*0.3f);

            gamePlayPage.GetChild(2).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(2).GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(0.65f, 0.65f);
            gamePlayPage.GetChild(3).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -height*0.2f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(-width*0.2f, height*0.2f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height*0.2f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(4).GetComponent<RectTransform>().anchoredPosition = new Vector2(width*0.2f, height*0.2f);
            gamePlayPage.GetChild(4).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(4).GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(0.6f, 0.6f);

        } else {
            float width = Screen.width;
            float height = Screen.height;
            startPage.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            startPage.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            startPage.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width*0.8f, 100);
            startPage.GetChild(1).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width/2, height/5);
            startPage.GetChild(1).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.1f, height*0.9f);
            startPage.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 50;

            authPage.GetChild(0).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            authPage.GetChild(1).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            authPage.GetChild(3).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            authPage.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            authPage.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 60;
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 55;
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 32;
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = 32;
            authPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.75f);
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.4f, height*0.6f);
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.4f+300, height*0.5f);
            authPage.GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f+200, height*0.15f);
            authPage.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.4f+50, height*0.4f);
            authPage.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.4f+50, height*0.3f);
            authPage.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.4f+50, height*0.2f);
            authPage.GetChild(0).GetChild(2).GetChild(2).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.4f+300, height*0.15f);
            authPage.GetChild(0).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.1f, height*0.95f);

            authPage.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            authPage.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 70;
            authPage.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 50;
            authPage.GetChild(1).GetChild(1).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.8f);
            authPage.GetChild(1).GetChild(1).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.7f);
            authPage.GetChild(1).GetChild(2).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.6f);
            authPage.GetChild(1).GetChild(2).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.5f);
            authPage.GetChild(1).GetChild(2).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.4f);
            authPage.GetChild(1).GetChild(2).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.3f);
            authPage.GetChild(1).GetChild(3).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.2f);
            authPage.GetChild(1).GetChild(3).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.1f, height*0.9f);

            authPage.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width*3, height*3);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width*0.8f, height*0.7f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(width*0.5f, height*0.5f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.5f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().fontSize = 70;
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().fontSize = 70;
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width*0.35f, height*0.01f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.65f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.35f, height*0.7f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.7f, height*0.7f);
            authPage.GetChild(3).GetChild(0).GetChild(0).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.48f);
        //authPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(700/4, 3*1600/5);
            menuPage.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            menuPage.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            menuPage.GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.6f);
            menuPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.4f);
            menuPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.9f, height*0.95f);
            menuPage.GetChild(0).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.3f, height*0.85f);
            menuPage.GetChild(0).GetChild(4).GetComponent<RectTransform>().transform.position = new Vector2(width*0.75f, height*0.85f);
            menuPage.GetChild(0).GetChild(5).GetComponent<RectTransform>().transform.position = new Vector2(width*0.85f, height*0.1f);
            menuPage.GetChild(0).GetChild(6).GetComponent<RectTransform>().transform.position = new Vector2(width*0.1f, height*0.95f);

            gamePlayPage.GetChild(0).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width*2, height*2);
            
            gamePlayPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().localScale = new Vector2(2, 2);
            gamePlayPage.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -0.1f*height);

            gamePlayPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().localScale = new Vector2(3f, 3f);
            gamePlayPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            gamePlayPage.GetChild(0).GetChild(2).GetComponent<RectTransform>().transform.position = new Vector2(width*0.58f, height*0.56f);
            gamePlayPage.GetChild(0).GetChild(5).GetComponent<RectTransform>().localScale = new Vector2(3f, 2.5f);
            gamePlayPage.GetChild(0).GetChild(5).GetComponent<RectTransform>().transform.position = new Vector2(width*0.5f, height*0.17f);
            gamePlayPage.GetChild(0).GetChild(6).GetComponent<RectTransform>().transform.position = new Vector2(width*0.1f, height*0.96f);
            gamePlayPage.GetChild(0).GetChild(3).GetComponent<RectTransform>().transform.position = new Vector2(width*0.7f, height*0.82f);
            gamePlayPage.GetChild(0).GetChild(4).GetComponent<RectTransform>().transform.position = new Vector2(width*0.3f, height*0.82f);
            gamePlayPage.GetChild(0).GetChild(7).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -height*0.3f);

            gamePlayPage.GetChild(2).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(2).GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(0.6f, 0.6f);
            gamePlayPage.GetChild(3).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -height * 0.2f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height*0.1f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height * 0.4f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height * 0.3f);
            gamePlayPage.GetChild(3).GetChild(0).GetChild(4).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height * 0.2f);
            gamePlayPage.GetChild(4).GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
            gamePlayPage.GetChild(4).GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(0.4f, 0.4f);
        }
    }
     public void OnClickCloseErrAlert(Transform trans)
    {
        trans.gameObject.SetActive(false);
        if(trans.name == "errAlert"){
            authPage.gameObject.SetActive(true);
            authPage.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ClickingSound() {
        // if(Input.GetMouseButtonDown(0)) {
            click.Play();
        // }
    }
}

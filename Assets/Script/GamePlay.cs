using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    static public GamePlay play;
    [Header("Letters")] [Tooltip("The letter-populated Box.")]
    public List<Button> letters;
    [Header("Word Box")] [Tooltip("The Word Box List that will be filled with words you make.")]
    public List<TMP_InputField> grids;
    private List<TextMeshProUGUI> clues = new List<TextMeshProUGUI>();
    private List<int> selected = new List<int>();
    private List<int> clue = new List<int>();
    public List<string> words = new List<string>();
    List<LocalServer.Save> savedData = new List<LocalServer.Save>();
    public GameObject title;
    public GameObject submit;
    public GameObject screen;
    public GameObject Alert;
    public GameObject GameOver;
    public GameObject GameWin;
    public Transform savedGame;
    public Transform smMenu;
    public Transform myGame;
    public Button reset;
    public Button submit;
    public int correctNumber = 0;
    private bool isCorrect = false;
    private int gridId = 0;
    private int from;
    private int norLossNum = 3;
    private int norClicker = 32;
    public int lossNum;
    public int clicker;
    public int score;
    public float timeRemaining;
    private bool timeIsRunning = false;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI clickerNum;
    public TextMeshProUGUI goesText;
    public Transform correctNum;
    public ColorBlock resetColor;

    void Start()
    {
        if(play == null) play = this;
        Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
        timeRemaining = 0;
        int i =0;
        for(int btn = 1; btn <= 4; btn++) {
            for (int cnt = 1; cnt <= 4; cnt++) {
                Button letter = gameObject.transform.GetChild(0).GetChild(2).GetChild(btn).GetChild(cnt).gameObject.GetComponent<Button>();
                letter.name = i++.ToString();
                letters.Add(letter);
                letter.onClick.AddListener(delegate{DisplayToGrid(int.Parse(letter.name));});
            }
            TMP_InputField grid = gameObject.transform.GetChild(0).GetChild(5).GetChild(btn-1).gameObject.GetComponent<TMP_InputField>();
            grids.Add(grid);
            TextMeshProUGUI q = gameObject.transform.GetChild(0).GetChild(5).GetChild(btn-1).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            clues.Add(q);
            //letters[btn-1].interactable = true;
        }
        title = GameObject.Find("Single/LetterBox/NUM");
        resetColor = letters[0].colors;
        resetColor.normalColor = new Color32(70, 70, 70, 255);
        resetColor.highlightedColor = new Color32(90, 90, 90, 255);
        resetColor.pressedColor = new Color32(110, 110, 110, 255);
        resetColor.disabledColor = new Color32(200, 200, 200, 255);
        resetColor.selectedColor = new Color32(70, 70, 70, 255);

        reset =gameObject.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>();
        reset.onClick.AddListener(delegate{ Reset(); });
        submit = gameObject.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<Button>();
        submit.onClick.AddListener(delegate { IsSucceed(); });
        gameObject.transform.GetChild(0).GetChild(2).GetChild(10).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(2).GetChild(6).gameObject.SetActive(true);
    }
    void Update()
    {
        if (timeIsRunning)
        {
            Clock();
            if (from != 0)
            {
                for (int i = 1; i < 5; i++)
                {
                    gameObject.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetComponent<Button>().interactable = false;
                }
                gameObject.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(gridId + 1).GetComponent<Button>().interactable = true;
            } else
            {
                for (int i = 1; i < 5; i++)
                {
                    if (grids[i-1].text.Length == 2)
                    {
                        gameObject.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        gameObject.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
    }
    public void GetWords() {
    //=====In network======//
        //GameAPI.request.FetchWords();
    //======In local=======//
        LocalServer.ls.FetchWords();
    }
    public void DisplayToBox(){
        List<int> temp = new List<int>();
        for(int i = 0; i<4; i++) 
            clue.Add(4);
        for(int row = 0; row < 4; row++){
            List<int> randomList = new List<int>();
            for (int cnt = 0; cnt < 4; cnt++) {
                int numToAdd = Random.Range(0, 4);
                while(randomList.Contains(numToAdd)){
                    numToAdd = Random.Range(0,4);
                }
                randomList.Add(numToAdd);
                if(row == 0) temp.Add(numToAdd);
            }
            for(int div = 0; div < 4; div++){
                letters[div+row*4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " "+words[randomList[div]][row];
                clue[randomList[div]] += div;
            }
        }
        for(int i = 0; i < 4; i++){
            clues[i].text = "= "+clue[temp[i]].ToString();
        }
    }
    public void DisplayToGrid(int id)
    {
        clicker++;
        UI_Main.UIM.ClickingSound();
        if (id > 3)
        {
            grids[gridId].text += letters[id].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            DisableLetters(id);
        }
        else
        {
            gridId = id;
            DisableLetters(id);
        }
    }
    public void DisableLetters(int id) {
        ChangeColor(id);
        for (int pointer = 0; pointer <= 15; pointer++)
        {
            letters[pointer].interactable = false;
        }
        if(id > 11) {
            from = 0;
            title.transform.GetChild(gridId+1).GetComponent<RectTransform>().sizeDelta = new Vector2(51, 51);
            gridId++;
        } else if (id == -1) {
            //for resetOne from = 0;
            if (from == 0)
            {
                from = 12;
            }
            else
            {
                from -= 4;
            }

        } else
        {
            from = ((int)id / 4 + 1) * 4;
        }
        for (int pointer = from; pointer <= from+3; pointer++) {
            if(!selected.Contains(pointer))
                    letters[pointer].interactable = true;
        }
        if(selected.Count == 16) {
            timeIsRunning = false;
            submit.gameObject.SetActive(true);
        }
    }
    public void IsSucceed() {
        for(int cnt = 0; cnt < 4; cnt++)
        {
            string check = grids[cnt].text.Replace(" ", string.Empty);
            if (words.Contains(check))
            {
                correctNumber++;
            }
        }
        if (correctNumber == 4)
        {
            isCorrect = true;
            ScoreCounter();
        }
        if (!isCorrect) {
            lossNum++;
        }
        BeforeReset();
    }

//==========Utilities==============//
    public void Clock()
    {
            timeRemaining += Time.deltaTime;
            DisplayTime(timeRemaining);
    }
    public void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay/60);
        float seconds = Mathf.FloorToInt(timeToDisplay%60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        clickerNum.text = clicker.ToString();
        goesText.text = (5-lossNum).ToString();
    }
    public void ChangeColor(int id)
    {
        if (id != -1)
        {
            selected.Add(id);
        }
        ColorBlock theColor = resetColor;
        if (gridId == 0)
        {
            theColor.normalColor = new Color32(65, 65, 65, 150);
            theColor.disabledColor = new Color32(15, 150, 255, 150);
        }
        else if (gridId == 1)
        {
            theColor.normalColor = new Color32(65, 65, 65, 150);
            theColor.disabledColor = new Color32(150, 15, 255, 150);
        }
        else if (gridId == 2)
        {
            theColor.normalColor = new Color32(65, 65, 65, 150);
            theColor.disabledColor = new Color32(100, 255, 100, 150);
        }
        else if (gridId == 3)
        {
            theColor.normalColor = new Color32(65, 65, 65, 150);
            theColor.disabledColor = new Color32(255, 200, 100, 150);
        }
        for (int cnt = 0; cnt < 16; cnt++)
        {
            if (selected.Contains(cnt))
            {
                if (letters[cnt].colors.disabledColor == new Color32(200, 200, 200, 255))
                {
                    letters[cnt].colors = theColor;
                }
            }
            else
            {
                letters[cnt].colors = resetColor;
            }
        }
    }
    public void ScoreCounter()
    {
        if (lossNum == 4) score += 10;
        else score += (4-lossNum)*25;
        PlayerPrefs.SetString("myScore", score.ToString());
    }
    public void BeforeReset(){
        submit.gameObject.SetActive(false);
        if (isCorrect)
        {
            gameObject.transform.GetChild(0).GetChild(2).GetChild(9).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ATTEMPTS "+lossNum+" OUT OF 5";
            gameObject.transform.GetChild(0).GetChild(2).GetChild(9).GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = score.ToString();
            gameObject.transform.GetChild(0).GetChild(2).GetChild(9).GetChild(3).GetComponent<Button>().interactable = true;
            gameObject.transform.GetChild(0).GetChild(2).GetChild(9).GetComponent<Animation>().Play("alertleft");
            gameObject.transform.GetChild(0).GetChild(2).GetChild(9).gameObject.SetActive(true);
            timeRemaining = 0;
            //timeIsRunning = false;
        }
        else if (lossNum > 4 ) {
            gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ATTEMPTS "+lossNum+" OUT OF 5";
            gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "YOU HAVE FOUND \n"+correctNumber+" WORDS OUT OF 4";
            gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetChild(3).GetComponent<Button>().interactable = true;
            gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetComponent<Animation>().Play("alertleft");
            gameObject.transform.GetChild(0).GetChild(2).GetChild(8).gameObject.SetActive(true);
            timeRemaining = 0;
            //timeIsRunning = false;
        } else
        {
            gameObject.transform.GetChild(0).GetChild(2).GetChild(7).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ATTEMPTS "+lossNum+" OUT OF 5";
            gameObject.transform.GetChild(0).GetChild(2).GetChild(7).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "YOU HAVE FOUND \n"+correctNumber+" WORDS OUT OF 4";
            gameObject.transform.GetChild(0).GetChild(2).GetChild(7).GetComponent<Animation>().Play("alertleft");
            gameObject.transform.GetChild(0).GetChild(2).GetChild(7).gameObject.SetActive(true);
            //timeIsRunning = false;
        }
    }
    public void Save() {
        List<char> Input = new List<char>();
        List<string> clue = new List<string>();
        for(int cnt = 0; cnt<4; cnt++)
        {
            clue.Add(clues[cnt].text);
            Debug.Log("clue" + clue[cnt]);
        }
        for (int pointer = 0; pointer < 16; pointer++) {
            Input.Add("s"[0]);//need to be professional
            Input[pointer] =
              letters[pointer].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text[1];
        }
        LocalServer.ls.SaveData(words, Input, clue);
        gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetChild(3).GetComponent<Button>().interactable = false;
        gameObject.transform.GetChild(0).GetChild(2).GetChild(9).GetChild(3).GetComponent<Button>().interactable = false;
        /*if(isCorrect) {
            Reset();
            UI_Main.UIM.gamePlayPage.gameObject.SetActive(false);
            UI_Main.UIM.menuPage.gameObject.SetActive(true);
        }*/
    }
    /*public void Retry()
    {
        lossNum = 0;
        Reset();
    }*/
    public void Restart()
    {
        lossNum = 0;
        timeRemaining = 0;
        GetWords();
        DisplayToBox();
        Reset();
        DisplayAdvert();
        // StartCoroutine(MenuAnimation("popdown2", gameObject.transform.GetChild(0).GetChild(5).GetChild(4)));
        gameObject.transform.GetChild(0).GetChild(5).GetChild(4).GetComponent<Animation>().Play("popdown2");

    }
    public void ShowMenu() {
        gameObject.transform.GetChild(0).GetChild(5).GetChild(4).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(2).GetChild(5).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(5).GetChild(4).GetComponent<Animation>().Play("popupbox2");
        StartCoroutine(MenuAnimation("SavedGamedown", gameObject.transform.GetChild(0).GetChild(2).GetChild(10)));
        StartCoroutine(MenuAnimation("alertright", gameObject.transform.GetChild(0).GetChild(2).GetChild(7)));
        StartCoroutine(MenuAnimation("alertright", gameObject.transform.GetChild(0).GetChild(2).GetChild(8)));
        StartCoroutine(MenuAnimation("alertright", gameObject.transform.GetChild(0).GetChild(2).GetChild(9)));
    }
    public void DisplayAdvert() {
        gameObject.transform.GetChild(0).GetChild(2).GetChild(6).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(7).GetChild(0).gameObject.SetActive(false);
        StartCoroutine(ShowingAdvert());
    }
    public void SavedGame() {
        GameObject prefabObj;
        savedData = LocalServer.ls.LoadData();
        for (int cnt = 0; cnt < savedData.Count; cnt++)
        {
            prefabObj = Instantiate(Resources.Load<GameObject>("prefab/myGame"));
            //prefabObj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { OpenSavedGame(); });
            prefabObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "GAME "+cnt.ToString("d3");
            prefabObj.transform.SetParent(GamePlay.play.myGame);
            prefabObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50-120 * cnt);
            prefabObj.transform.localScale = Vector2.one;
            gameObject.transform.GetChild(0).GetChild(2).GetChild(10).gameObject.SetActive(true);
            OpenSavedGame(cnt);
        }
        StartCoroutine(MenuAnimation("popdown2", UI_Main.UIM.gamePlayPage.GetChild(0).GetChild(5).GetChild(4)));
        transform.GetChild(0).GetChild(2).GetChild(10).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(2).GetChild(10).GetComponent<Animation>().Play("SavedGameup");
        
    }
    public void OpenSavedGame(int index)
    {
        myGame.GetChild(index).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { OpenFromSave(index); DisplayAdvert(); });
    }
    public void OpenFromSave(int index) {
        Debug.Log(index);
        words.Clear();
        for(int cnt = 0; cnt <4; cnt++)
        {
            clues[cnt].text = savedData[index].clue[cnt];
            words.Add(savedData[index].wordList[cnt]);
        }
        for (int cnt = 0; cnt < 16; cnt++)
        {
            letters[cnt].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " " + savedData[index].letters[cnt];
        }
        lossNum = 0;
        Reset();
    }
    public void Reset() {
        for(int pointer = 4; pointer <= 15; pointer++) {
            letters[pointer].interactable = false;
            letters[pointer].colors = resetColor;
        }
        for(int pointer = 0; pointer <= 3; pointer++) {
            letters[pointer].interactable = true;
            letters[pointer].colors = resetColor;
            grids[pointer].text = letters[pointer].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
        for(int pointer = 1; pointer <= 4; pointer++)
        {
            title.transform.GetChild(pointer).GetComponent<RectTransform>().sizeDelta = new Vector2(49, 49);
        }
        gridId = 0;
        clicker = 0;
        correctNumber = 0;
        if (PlayerPrefs.GetString("myScore") != null)
            score = int.Parse(PlayerPrefs.GetString("myScore"));
        selected.Clear();
        clue.Clear();
        isCorrect = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(2).GetChild(6).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(2).GetChild(10).gameObject.SetActive(false);
        StartCoroutine(MenuAnimation("alertright", gameObject.transform.GetChild(0).GetChild(2).GetChild(7)));
        StartCoroutine(MenuAnimation("alertright", gameObject.transform.GetChild(0).GetChild(2).GetChild(8)));
        StartCoroutine(MenuAnimation("alertright", gameObject.transform.GetChild(0).GetChild(2).GetChild(9)));
        timeIsRunning = true;
    }
    //Useless now.
    public void ResetOne(int id)
    {
        gridId = id;
        string check = grids[gridId].text.Replace(" ", string.Empty);
        for (int cnt = 0; cnt < selected.Count; cnt += 4)
        {
            if (selected[cnt] == id)
            {
                for (int limit = check.Length; limit > 0; limit--)
                {
                    selected.RemoveAt(cnt);
                }
            }
        }
        DisableLetters(-1);
        grids[gridId].text = letters[id].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        title.transform.GetChild(gridId + 1).GetComponent<RectTransform>().sizeDelta = new Vector2(49, 49);
        from = 0;
        submit.gameObject.SetActive(false);
    }
    //============================
    public void BackSpace(int id)
    {
        gridId = id;
        if (grids[gridId].text.Length == 8)
        {
            for (int cnt =0; cnt<selected.Count; cnt += 4)
            {
                Debug.Log("up"+selected[cnt]);
                if(selected[cnt] == id)
                {
                    for(int next = 0; next<4; next++)
                    {
                        int temp = selected[cnt + next];
                        selected[cnt + next] = selected[selected.Count + next - 4];
                        selected[selected.Count + next - 4] = temp;
                    }
                }
                Debug.Log("down"+selected[cnt]);
            }
        }
        selected.RemoveAt(selected.Count - 1);
        grids[gridId].text = grids[gridId].text.Substring(0, grids[gridId].text.Length - 2);
        DisableLetters(-1);
        if (selected.Count % 4 == 0)
        {
            grids[gridId].text = letters[gridId].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
    }
    public IEnumerator MenuAnimation(string action, Transform body) {
            body.GetComponent<Animation>().Play(action);
            yield return new WaitForSeconds(1);
            body.gameObject.SetActive(false);
    }

    public IEnumerator ShowingAdvert() {
        int cnt = 1;
        while(timeIsRunning)
        {
            cnt++;
            gameObject.transform.GetChild(0).GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>("advert/ad"+cnt%10);
            yield return new WaitForSeconds(10);
        }
    }

}

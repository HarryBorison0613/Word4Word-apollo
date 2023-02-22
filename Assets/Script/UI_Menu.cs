using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
    }
    public void ToSinglePlay() {
        UI_Main.UIM.gamePlayPage.GetChild(0).GetChild(2).GetChild(7).gameObject.SetActive(false);
        UI_Main.UIM.gamePlayPage.GetChild(0).GetChild(2).GetChild(8).gameObject.SetActive(false);
        UI_Main.UIM.gamePlayPage.GetChild(0).GetChild(5).GetChild(4).gameObject.SetActive(true);
        UI_Main.UIM.gamePlayPage.GetChild(0).GetChild(5).GetChild(4).GetComponent<Animation>().Play("popupbox2");
        UI_Main.UIM.gamePlayPage.GetChild(0).gameObject.SetActive(true);
        UI_Main.UIM.gamePlayPage.gameObject.SetActive(true);
        UI_Main.UIM.menuPage.gameObject.SetActive(false);
    }
}

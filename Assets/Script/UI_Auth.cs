using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Auth : MonoBehaviour
{
    static public UI_Auth UIA;
    public Transform errorAlert;
    public Button loginBtn;
    public Button toRegisterBtn;
    public Button registerBtn;

    void Start()
    {
        if (UIA == null) UIA = this;
        Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
        loginBtn.onClick.AddListener(LogIn);
        toRegisterBtn.onClick.AddListener(ToRegister);
        registerBtn.onClick.AddListener(Register);
    }

    public void Register() {
        string name = GameObject.Find("Register/Input/name").transform.GetComponent<TMP_InputField>().text;
        string email = GameObject.Find("Register/Input/email-Input").transform.GetComponent<TMP_InputField>().text;
        string pwd = GameObject.Find("Register/Input/pwd-Input").transform.GetComponent<TMP_InputField>().text;
        string confPwd = GameObject.Find("Register/Input/confPwd-Input").transform.GetComponent<TMP_InputField>().text;
        InputValidation(name, email, pwd, confPwd, "REGISTER");
    }
    public void LogIn() {
        string email = GameObject.Find("LogIn/Side2/Input/email-Input").transform.GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("LogIn/Side2/Input/pwd-Input").transform.GetComponent<TMP_InputField>().text;

        InputValidation("name", email, password, password, "LOGIN");
    }
    public void ToRegister() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void InputValidation(string name, string email, string pwd, string confPwd, string flag) {
        string[] a_count = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
        string[] dot_count = null;
        if (a_count.Length > 1)
            dot_count = a_count[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (string.IsNullOrEmpty(name))
        {
            SetErrAlertText(gameProperty.NAME_INPUT_ERROR);
            errorAlert.gameObject.SetActive(true);
            return;
        }
        if (string.IsNullOrEmpty(email))
        {
            SetErrAlertText(gameProperty.EMAIL_INPUT_EMPTY);
            errorAlert.gameObject.SetActive(true);
            return;
        }
        else
        {
            if (dot_count == null || dot_count.Length < 2)
            {
                SetErrAlertText(gameProperty.EMAIL_INPUT_ERROR);
                errorAlert.gameObject.SetActive(true);
                return;
            }
            if (a_count.Length > 2)
            {
                SetErrAlertText(gameProperty.EMAIL_INPUT_ERROR);
                errorAlert.gameObject.SetActive(true);
                return;
            }
        }
            if (string.IsNullOrEmpty(pwd))
            {
                SetErrAlertText(gameProperty.PASSWORD_INPUT_EMPTY);
                errorAlert.gameObject.SetActive(true);
                return;
            }
            if (pwd != confPwd)
            {
                SetErrAlertText(gameProperty.PASSWORD_INPUT_ERROR);
                errorAlert.gameObject.SetActive(true);
                return;
            }
            if (pwd.Length < 8)
            {
                SetErrAlertText(gameProperty.PASSWORD_LESS_CHARACTERS);
                errorAlert.gameObject.SetActive(true);
                return;
            }
        if(flag == "REGISTER"){
        //======In network=======//
            //GameAPI.request.LogIn(name, email, pwd, flag);
        //======In Local=========//
            LocalServer.ls.Auth(name, email, pwd, flag);
            UI_Main.UIM.InitializeInputField(1,-1,-1,4);
        } else if (flag == "LOGIN") {
        //=======In network======//
            //GameAPI.request.LogIn(string.Empty, email, pwd, flag);
        //=========In local======//
            LocalServer.ls.Auth(name, email, pwd, flag);
            UI_Main.UIM.InitializeInputField(0,2,1,2);
        }
    }
    public void SetErrAlertText(string errorContent)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        errorAlert.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = errorContent;
        errorAlert.gameObject.SetActive(true);
    }
}

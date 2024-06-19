using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    VisualElement root;
    VisualElement errorBox;
    TextField passwordTextField;
    TextField emailTextField;
    Button button;
    Label errorBoxLabel;
    bool emailError;
    bool passwordError;
    
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        emailTextField = root.Q<TextField>("EmailTextField");
        passwordTextField = root.Q<TextField>("PasswordTextField");
        button = root.Q<Button>("LoginButton");
        errorBox = root.Q<VisualElement>("ErrorBox");
        errorBoxLabel = root.Q<Label>("ErrorBoxLabel");
        ErrorBoxReset();

        button.clickable.clicked += OnButtonClickedHandler;
    }

    void OnButtonClickedHandler()
    {
        if(emailTextField.text == "admin")
        {
            Debug.Log(emailTextField.text);
        }
        else
        {
            emailError = true;
            Debug.Log("email error");
        }
        if(passwordTextField.text == "admin")
        {
            Debug.Log(passwordTextField.text);
        }
        else
        {
            passwordError = true;
            Debug.Log("password error");
        }

        if(emailError == true && passwordError == false)
        {
            errorBoxLabel.text = "email error";
            errorBox.visible = true;
            errorBoxLabel.visible = true;
            Invoke("ErrorBoxReset", 2f);
        }
        else if(emailError == false && passwordError == true)
        {
            errorBoxLabel.text = "password error";
            errorBox.visible = true;
            errorBoxLabel.visible = true;
            Invoke("ErrorBoxReset", 2f);
        }
        else if(emailError == true && passwordError == true)
        {
            errorBoxLabel.text = "password and email error";
            errorBox.visible = true;
            errorBoxLabel.visible = true;
            Invoke("ErrorBoxReset", 2f);
        }
    }

    void ErrorBoxReset()
    {
        errorBox.visible = false;
        errorBoxLabel.visible = false;
        emailError = false;
        passwordError = false;
    }
}
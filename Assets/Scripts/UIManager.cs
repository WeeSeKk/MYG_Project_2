using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    VisualElement root;
    VisualElement tab;
    VisualElement errorBox;
    TextField passwordTextField;
    TextField emailTextField;
    Button button;
    Label errorBoxLabel;
    string[] emails = {"admin","admin1"};
    string[] passwords = {"admin","admin1"};
    
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        emailTextField = root.Q<TextField>("EmailTextField");
        passwordTextField = root.Q<TextField>("PasswordTextField");
        button = root.Q<Button>("LoginButton");
        errorBox = root.Q<VisualElement>("ErrorBox");
        tab = root.Q<VisualElement>("TAB");
        errorBoxLabel = root.Q<Label>("ErrorBoxLabel");

        button.RegisterCallback<ClickEvent>(evt => StartCoroutine(OnLoginButtonClicked(evt)));

        errorBox.AddToClassList("errorBoxUp");
        tab.RemoveFromClassList("tabOut");
    }

    IEnumerator OnLoginButtonClicked(ClickEvent evt)
    {
        errorBox.AddToClassList("errorBoxUp");

        bool emailValid = emails.Contains(emailTextField.text);
        bool passwordValid = passwords.Contains(passwordTextField.text);

        switch (emailValid, passwordValid)
        {
            case (false, true):
                errorBoxLabel.text = "email invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (true, false):
                errorBoxLabel.text = "password invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (false, false):
                errorBoxLabel.text = "password & email invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (true, true):
                tab.AddToClassList("tabOut");
                break;
        }
        
        errorBox.AddToClassList("errorBoxUp");
    }
}
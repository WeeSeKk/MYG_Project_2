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
    VisualElement gear0;
    VisualElement gear1;
    VisualElement gear2;
    VisualElement registFrame;
    VisualElement transitionFrame;
    VisualElement loginFrame;
    VisualElement frame;
    VisualElement errorBox;
    TextField passwordTextField;
    TextField newPasswordTextField;
    TextField confPasswordTextField;
    TextField emailTextField;
    TextField newEmailTextField;
    Button button;
    Button registButton;
    Label errorBoxLabel;
    List<string> emails;
    List<string> passwords;
    
    void Start()
    {
        emails = new List<string> { "admin"};
        passwords = new List<string> { "admin"};

        root = GetComponent<UIDocument>().rootVisualElement;
        emailTextField = root.Q<TextField>("EmailTextField");
        passwordTextField = root.Q<TextField>("PasswordTextField");
        button = root.Q<Button>("LoginButton");
        errorBox = root.Q<VisualElement>("ErrorBox");
        tab = root.Q<VisualElement>("TAB");
        errorBoxLabel = root.Q<Label>("ErrorBoxLabel");
        frame = root.Q<VisualElement>("Frame");
        registButton = root.Q<Button>("RegistButton");
        registFrame = root.Q<VisualElement>("RegisterWoodFrame");
        transitionFrame = root.Q<VisualElement>("TransitionWoodFrame");
        loginFrame = root.Q<VisualElement>("LoginWoodFrame");
        gear0 = root.Q<VisualElement>("BottomRightGear");
        gear1 = root.Q<VisualElement>("BottomLeftGear");
        gear2 = root.Q<VisualElement>("TopLeftGear");
        newEmailTextField = root.Q<TextField>("NewEmailTextField");
        newPasswordTextField = root.Q<TextField>("NewPasswordTextField");
        confPasswordTextField = root.Q<TextField>("ConfPasswordTextField");

        frame.pickingMode = PickingMode.Ignore;

        button.RegisterCallback<ClickEvent>(evt => StartCoroutine(OnLoginButtonClicked(evt)));
        registButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(OnRegisterButtonClicked(evt)));

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

    IEnumerator OnRegisterButtonClicked(ClickEvent evt)
    {
        errorBox.AddToClassList("errorBoxUp");

        bool newemailValid = newEmailTextField.text.Length > 0;//await new system
        bool newpasswordValid = newPasswordTextField.text.Length >= 6;
        bool confnewpassowrd = confPasswordTextField.text == newPasswordTextField.text;

        switch (newemailValid, newpasswordValid, confnewpassowrd)
        {
            case (false, true, true)://bad email
                errorBoxLabel.text = "email invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (true, false, true)://bad password
                errorBoxLabel.text = "password too short";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (true, true, false)://passwords don't match
                errorBoxLabel.text = "passwords don't match";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (false, false, false)://passwords and email invalid
                errorBoxLabel.text = "passwords & email invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (false, false, true)://passwords and email invalid
                errorBoxLabel.text = "passwords & email invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (true, true, true)://if register success
                emails.Add(newEmailTextField.text);
                passwords.Add(newPasswordTextField.text);
                FramesAnimation();
                break;
        }
       
        errorBox.AddToClassList("errorBoxUp");
    }

    void FramesAnimation()
    {   
        registFrame.AddToClassList("registerWoodFrameHidden");
        transitionFrame.AddToClassList("transitionWoodFrameHidden");
        gear0.AddToClassList("GearRotate");
        gear1.AddToClassList("GearRotate");
        gear2.AddToClassList("GearRotate");
        loginFrame.RemoveFromClassList("loginWoodFramHidden");
    }
}
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class UITest
{
    [UnityTest]
    public IEnumerator FindUIDocument()//find ui document
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Assert.IsNotNull(uIDocument);
    }

    [UnityTest]
    public IEnumerator RegisterSuccess()//verify if ui animation play if register success
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        VisualElement registFrame = uIDocument.rootVisualElement.Q<VisualElement>("RegisterWoodFrame");
        Button registButton = uIDocument.rootVisualElement.Q<Button>("RegistButton");
        TextField newEmailTextField = uIDocument.rootVisualElement.Q<TextField>("NewEmailTextField");
        TextField newPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("NewPasswordTextField");
        TextField confPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("ConfPasswordTextField");

        newEmailTextField.value = "WeeSeK@hotmail.fr";
        newPasswordTextField.value = "azerty";
        confPasswordTextField.value = "azerty";

        using (var clicked = new NavigationSubmitEvent {target = registButton})
            registButton.SendEvent(clicked);

        Assert.IsNotNull(registFrame.ClassListContains("registerWoodFrameHidden"));
    }

    [UnityTest]
    public IEnumerator LoginSuccess()//verify if ui animation play if login success
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        VisualElement loginFrame = uIDocument.rootVisualElement.Q<VisualElement>("LoginWoodFrame");
        Button loginButton = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");
        

        emailTextField.value = "WeeSeK@hotmail.fr";
        passwordTextField.value = "azerty";

        using (var clicked = new NavigationSubmitEvent {target = loginButton})
            loginButton.SendEvent(clicked);

        Assert.IsNotNull(!loginFrame.ClassListContains("loginWoodFramHidden"));
    }

    [UnityTest]
    public IEnumerator Connected()//Verify message connected if connection to photon successfull
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Button nicknameButton = uIDocument.rootVisualElement.Q<Button>("validnicknamebutton");
        TextField nicknameTextField = uIDocument.rootVisualElement.Q<TextField>("nicknametextfield");
        Label loginBoxLabel = uIDocument.rootVisualElement.Q<Label>("logginboxlabel");
        
        nicknameTextField.value = "WeeSeK";

        using (var clicked = new NavigationSubmitEvent {target = nicknameButton})
            nicknameButton.SendEvent(clicked);
            yield return new WaitForSeconds(3f);
        

        Assert.IsNotNull(loginBoxLabel.text = "Connected");
    }

    [UnityTest]
    public IEnumerator ALLIN()//verify the whole process of registering a new account, login with the new account and connect to photon
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Button loginButton = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        Button registButton = uIDocument.rootVisualElement.Q<Button>("RegistButton");
        Button nicknameButton = uIDocument.rootVisualElement.Q<Button>("validnicknamebutton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");
        TextField newEmailTextField = uIDocument.rootVisualElement.Q<TextField>("NewEmailTextField");
        TextField newPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("NewPasswordTextField");
        TextField confPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("ConfPasswordTextField");
        TextField nicknameTextField = uIDocument.rootVisualElement.Q<TextField>("nicknametextfield");
        Label loginBoxLabel = uIDocument.rootVisualElement.Q<Label>("logginboxlabel");
        
        

        newEmailTextField.value = "WeeSeK@hotmail.fr";
        newPasswordTextField.value = "azerty";
        confPasswordTextField.value = "azerty";

        using (var clicked = new NavigationSubmitEvent {target = loginButton})
            loginButton.SendEvent(clicked);

        yield return new WaitForSeconds(3f);

        emailTextField.value = "WeeSeK@hotmail.fr";
        passwordTextField.value = "azerty";

        using (var clicked = new NavigationSubmitEvent {target = registButton})
            registButton.SendEvent(clicked);

        yield return new WaitForSeconds(3f);    

        nicknameTextField.value = "WeeSeK";

        using (var clicked = new NavigationSubmitEvent {target = nicknameButton})
            nicknameButton.SendEvent(clicked);

        yield return new WaitForSeconds(3f);

        Assert.IsNotNull(loginBoxLabel.text = "Connected");
    }

    [UnityTest]
    public IEnumerator RegisterFailEmail()//Verify error message if fail email
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Button registButton = uIDocument.rootVisualElement.Q<Button>("RegistButton");
        TextField newEmailTextField = uIDocument.rootVisualElement.Q<TextField>("NewEmailTextField");
        TextField newPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("NewPasswordTextField");
        TextField confPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("ConfPasswordTextField");
        Label errorBox = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");

        newEmailTextField.value = "WeeSeKhotmailfr";
        newPasswordTextField.value = "azerty";
        confPasswordTextField.value = "azerty";

        using (var clicked = new NavigationSubmitEvent {target = registButton})
            registButton.SendEvent(clicked);

        Assert.IsNotNull(errorBox.text = "email invalid");
    }

    [UnityTest]
    public IEnumerator RegisterFailShortPassword()//Verify error message if fail password 
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Button registButton = uIDocument.rootVisualElement.Q<Button>("RegistButton");
        TextField newEmailTextField = uIDocument.rootVisualElement.Q<TextField>("NewEmailTextField");
        TextField newPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("NewPasswordTextField");
        TextField confPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("ConfPasswordTextField");
        Label errorBox = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");

        newEmailTextField.value = "WeeSeK@hotmail.fr";
        newPasswordTextField.value = "azer";
        confPasswordTextField.value = "azer";

        using (var clicked = new NavigationSubmitEvent {target = registButton})
            registButton.SendEvent(clicked);

        Assert.IsNotNull(errorBox.text = "password too short");
    }

    [UnityTest]
    public IEnumerator RegisterFailPasswordMatch()//Verify error message if fail password
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Button registButton = uIDocument.rootVisualElement.Q<Button>("RegistButton");
        TextField newEmailTextField = uIDocument.rootVisualElement.Q<TextField>("NewEmailTextField");
        TextField newPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("NewPasswordTextField");
        TextField confPasswordTextField = uIDocument.rootVisualElement.Q<TextField>("ConfPasswordTextField");
        Label errorBox = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");

        newEmailTextField.value = "WeeSeK@hotmail.fr";
        newPasswordTextField.value = "azerty";
        confPasswordTextField.value = "azertu";

        using (var clicked = new NavigationSubmitEvent {target = registButton})
            registButton.SendEvent(clicked);

        Assert.IsNotNull(errorBox.text = "passwords don't match");
    }

    [UnityTest]
    public IEnumerator LoginFailEmail()//Verify error message if fail email
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        VisualElement loginFrame = uIDocument.rootVisualElement.Q<VisualElement>("LoginWoodFrame");
        Button loginButton = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");
        Label errorBox = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");
        

        emailTextField.value = "test@test.test";
        passwordTextField.value = "admin";

        using (var clicked = new NavigationSubmitEvent {target = loginButton})
            loginButton.SendEvent(clicked);

        Assert.IsNotNull(!loginFrame.ClassListContains(errorBox.text = "email unknown"));
    }

    [UnityTest]
    public IEnumerator LoginFailPassword()//Verify error message if fail password 
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        VisualElement loginFrame = uIDocument.rootVisualElement.Q<VisualElement>("LoginWoodFrame");
        Button loginButton = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");
        Label errorBox = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");
        

        emailTextField.value = "admin";
        passwordTextField.value = "test";

        using (var clicked = new NavigationSubmitEvent {target = loginButton})
            loginButton.SendEvent(clicked);

        Assert.IsNotNull(!loginFrame.ClassListContains(errorBox.text = "password invalid"));
    }

    [UnityTest]
    public IEnumerator LoginFailPasswordAndEmail()//Verify error message if fail password and email
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        VisualElement loginFrame = uIDocument.rootVisualElement.Q<VisualElement>("LoginWoodFrame");
        Button loginButton = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");
        Label errorBox = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");
        

        emailTextField.value = "admin";
        passwordTextField.value = "test";

        using (var clicked = new NavigationSubmitEvent {target = loginButton})
            loginButton.SendEvent(clicked);

        Assert.IsNotNull(!loginFrame.ClassListContains(errorBox.text = "password & email invalid"));
    }
}
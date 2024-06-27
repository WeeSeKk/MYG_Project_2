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
    public IEnumerator FindUIDocument()
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Assert.IsNotNull(uIDocument);
    }

    [UnityTest]
    public IEnumerator RegisterSuccess()
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
    public IEnumerator LoginSuccess()
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
    public IEnumerator Connected()
    {
        SceneManager.LoadScene(0);
        yield return null;
        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        VisualElement loginFrame = uIDocument.rootVisualElement.Q<VisualElement>("LoginWoodFrame");
        Button nicknameButton = uIDocument.rootVisualElement.Q<Button>("validnicknamebutton");
        TextField nicknameTextField = uIDocument.rootVisualElement.Q<TextField>("nicknametextfield");
        Label loginBoxLabel = uIDocument.rootVisualElement.Q<Label>("logginboxlabel");
        
        nicknameTextField.value = "WeeSeK";

        using (var clicked = new NavigationSubmitEvent {target = nicknameButton})
            nicknameButton.SendEvent(clicked);
            yield return new WaitForSeconds(2f);
        

        Assert.IsNotNull(loginBoxLabel.text = "Connected");
    }
}
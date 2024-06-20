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
    public IEnumerator PasswordAndEmailErrorLabel()
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Label label = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");
        Button button = uIDocument.rootVisualElement.Q<Button>("LoginButton");

        using (var clicked = new NavigationSubmitEvent {target = button})
            button.SendEvent(clicked);

        Assert.IsTrue(label.text == "password and email error");
    }

    [UnityTest]
    public IEnumerator EmailErrorLabel()
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Label label = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");
        Button button = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");

        emailTextField.value = "1234";
        passwordTextField.value = "admin";
        

        using (var clicked = new NavigationSubmitEvent {target = button})
            button.SendEvent(clicked);

        Assert.IsTrue(label.text == "email error");
    }

    [UnityTest]
    public IEnumerator PasswordErrorLabel()
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Label label = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");
        Button button = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");

        emailTextField.value = "admin";
        passwordTextField.value = "1234";
        

        using (var clicked = new NavigationSubmitEvent {target = button})
            button.SendEvent(clicked);

        Assert.IsTrue(label.text == "password error");
    }

    [UnityTest]
    public IEnumerator LoginLabel()
    {
        SceneManager.LoadScene(0);
        yield return null;

        GameObject go = GameObject.Find("UIDocument");
        UIDocument uIDocument = go.GetComponent<UIDocument>();
        Label label = uIDocument.rootVisualElement.Q<Label>("ErrorBoxLabel");
        Button button = uIDocument.rootVisualElement.Q<Button>("LoginButton");
        TextField emailTextField = uIDocument.rootVisualElement.Q<TextField>("EmailTextField");
        TextField passwordTextField = uIDocument.rootVisualElement.Q<TextField>("PasswordTextField");

        emailTextField.value = "admin";
        passwordTextField.value = "admin";
        

        using (var clicked = new NavigationSubmitEvent {target = button})
            button.SendEvent(clicked);

        Assert.IsTrue(label.text == "Label");
    }
}
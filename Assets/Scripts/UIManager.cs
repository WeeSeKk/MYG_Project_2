using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class UIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] VisualTreeAsset elementList;
    [SerializeField] UIDocument loggedMenu;
    [SerializeField] NetworkManager networkManager;
    ListView list;
    VisualElement root;
    VisualElement loginBox;
    VisualElement tab;
    VisualElement gear0;
    VisualElement gear1;
    VisualElement gear2;
    VisualElement registFrame;
    VisualElement transitionFrame;
    VisualElement loginFrame;
    VisualElement frame;
    VisualElement errorBox;
    Label loginBoxLabel;
    TextField passwordTextField;
    TextField newPasswordTextField;
    TextField confPasswordTextField;
    TextField emailTextField;
    TextField newEmailTextField;
    Button button;
    Button disconnectButton;
    Button skipButton;
    Button registButton;
    Label errorBoxLabel;
    List<string> emails;
    List<string> passwords;
    List<int> loggedCount = new List<int>();
    
    void Start()
    {
        emails = new List<string> { "admin"};
        passwords = new List<string> { "admin"};

        root = GetComponent<UIDocument>().rootVisualElement;
        list = root.Q<ListView>("loggedlist");
        emailTextField = root.Q<TextField>("EmailTextField");
        passwordTextField = root.Q<TextField>("PasswordTextField");
        button = root.Q<Button>("LoginButton");
        skipButton = root.Q<Button>("SkipButton");
        errorBox = root.Q<VisualElement>("ErrorBox");
        tab = root.Q<VisualElement>("TAB");
        errorBoxLabel = root.Q<Label>("ErrorBoxLabel");
        loginBoxLabel = root.Q<Label>("logginboxlabel");
        frame = root.Q<VisualElement>("Frame");
        registButton = root.Q<Button>("RegistButton");
        disconnectButton = root.Q<Button>("disconnectbutton");
        registFrame = root.Q<VisualElement>("RegisterWoodFrame");
        transitionFrame = root.Q<VisualElement>("TransitionWoodFrame");
        loginFrame = root.Q<VisualElement>("LoginWoodFrame");
        gear0 = root.Q<VisualElement>("BottomRightGear");
        gear1 = root.Q<VisualElement>("BottomLeftGear");
        gear2 = root.Q<VisualElement>("TopLeftGear");
        loginBox = root.Q<VisualElement>("logginbox");
        newEmailTextField = root.Q<TextField>("NewEmailTextField");
        newPasswordTextField = root.Q<TextField>("NewPasswordTextField");
        confPasswordTextField = root.Q<TextField>("ConfPasswordTextField");

        frame.pickingMode = PickingMode.Ignore;

        button.RegisterCallback<ClickEvent>(evt => StartCoroutine(OnLoginButtonClicked(evt)));
        registButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(OnRegisterButtonClicked(evt)));
        disconnectButton.RegisterCallback<ClickEvent>(networkManager.Disconnect);

        skipButton.clicked += FramesAnimation;

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
            case (false, true)://email not regist
                errorBoxLabel.text = "email unknown";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (true, false)://password not regist
                errorBoxLabel.text = "password invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (false, false)://both fail
                errorBoxLabel.text = "password & email invalid";
                errorBox.RemoveFromClassList("errorBoxUp");
                yield return new WaitForSeconds(2f);
                break;

            case (true, true)://connected
                networkManager.Connect();
                tab.AddToClassList("tabOut");
                break;
        }
       
        errorBox.AddToClassList("errorBoxUp");
    }

    bool IsEmailValid(string email)
    {
        const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|fr)$";
        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }

    IEnumerator OnRegisterButtonClicked(ClickEvent evt)
    {
        errorBox.AddToClassList("errorBoxUp");

        bool newemailValid = IsEmailValid(newEmailTextField.text);
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

    public IEnumerator LogginStatusBox(string text)
    {
        disconnectButton.AddToClassList("disconnectbuttonup");

        if(text == "Disconnected")
        {
            disconnectButton.RemoveFromClassList("disconnectbuttonup");
        }

        loginBoxLabel.text = text;
        loginBox.AddToClassList("logginboxdown");
        yield return new WaitForSeconds(2f);
        loginBox.RemoveFromClassList("logginboxdown");
    }

    [PunRPC]
    void AddList()
    {
        loggedCount.Add(loggedCount.Count);

        list.Clear();

        list.makeItem = () =>  elementList.CloneTree();
        list.itemsSource = loggedCount;
        list.bindItem = (root, i) =>
        {
            i += 1;
            root.Q<Label>().text = i.ToString() + " Account Connected";
            PhotonNetwork.LocalPlayer.NickName ="TEST";
        };
        list.fixedItemHeight = 60;
        list.Rebuild();
    }

    public override void OnJoinedRoom()
    {
        photonView.RPC("AddList", RpcTarget.All);
    }
}
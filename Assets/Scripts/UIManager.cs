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
    [SerializeField] NetworkManager networkManager;
    [SerializeField] SaveData saveData;
    ListView list;
    VisualElement root;
    VisualElement loginBox;
    VisualElement nicknameHolder;
    VisualElement progressBarProgress;
    VisualElement tab;
    VisualElement gear0;
    VisualElement gear1;
    VisualElement gear2;
    VisualElement registFrame;
    VisualElement transitionFrame;
    VisualElement loginFrame;
    VisualElement frame;
    ProgressBar progressBar;
    VisualElement errorBox;
    TextField passwordTextField;
    TextField newPasswordTextField;
    TextField confPasswordTextField;
    TextField emailTextField;
    TextField newEmailTextField;
    TextField nicknameTextField;
    Button button;
    Button nicknameButton;
    Button disconnectButton;
    Button skipButton;
    Button registButton;
    Label errorBoxLabel;
    Label loginBoxLabel;
    List<string> emails;
    List<string> passwords;
    List<string> usernames = new List<string>();
    
    void Start()
    {
        emails = new List<string> {"admin"};//setup admin a valid email for debug
        passwords = new List<string> {"admin"};//setup admin a valid password for debug

        root = GetComponent<UIDocument>().rootVisualElement;
        list = root.Q<ListView>("loggedlist");
        emailTextField = root.Q<TextField>("EmailTextField");
        passwordTextField = root.Q<TextField>("PasswordTextField");
        button = root.Q<Button>("LoginButton");
        nicknameButton = root.Q<Button>("validnicknamebutton");
        skipButton = root.Q<Button>("SkipButton");
        errorBox = root.Q<VisualElement>("ErrorBox");
        tab = root.Q<VisualElement>("TAB");
        nicknameHolder = root.Q<VisualElement>("nicknameelementsholder");
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
        nicknameTextField = root.Q<TextField>("nicknametextfield");
        progressBar = root.Q<ProgressBar>("ProgressBar");
        progressBarProgress = root.Q<VisualElement>("", "unity-progress-bar__progress");

        frame.pickingMode = PickingMode.Ignore;//ignore pickingmode for a visual element placed in front of the buttons and textfields so they are selectable

        button.RegisterCallback<ClickEvent>(evt => StartCoroutine(OnLoginButtonClicked()));
        registButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(OnRegisterButtonClicked()));
        disconnectButton.RegisterCallback<ClickEvent>(evt => networkManager.Disconnect());
        skipButton.RegisterCallback<ClickEvent>(evt => FramesAnimation());
        nicknameButton.RegisterCallback<ClickEvent>(evt => ConnectAndValidNickname());

        newPasswordTextField.RegisterValueChangedCallback(evt => ProgressBarUpdate());

        errorBox.AddToClassList("errorBoxUp");//setup error box 
        tab.RemoveFromClassList("tabOut");//setup tab
    }

    void ConnectAndValidNickname()//call the fonction to save the nickname and the on to connect to photon
    {
        networkManager.Connect();
        NetworkManager.SaveNickname(nicknameTextField.text);
        nicknameHolder.AddToClassList("nicknameelementsholderhidden");
    }

    void ProgressBarUpdate()//update the new password progress bar
    {
        if(progressBar.value < 2)//if value is 0/2 color red
        {
            progressBarProgress.style.backgroundColor = Color.red;
        }
        else if (progressBar.value > 2 && progressBar.value < 5)//if value is 2/5 color yellow
        {
            progressBarProgress.style.backgroundColor = Color.yellow;
        }
        else if (progressBar.value >= 5)//if value is 6 color green
        {
            progressBarProgress.style.backgroundColor = Color.green;
        }

        progressBar.value = newPasswordTextField.value.Length; 
    }

    IEnumerator OnLoginButtonClicked()//verify every case on the login tab
    {
        errorBox.AddToClassList("errorBoxUp");

        bool emailValid = IsUserExist(emailTextField.text, passwordTextField.text ,1);
        bool passwordValid = IsUserExist(emailTextField.text, passwordTextField.text ,2);

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

            case (true, true)://Logged in 
                tab.AddToClassList("tabOut");
                nicknameHolder.RemoveFromClassList("nicknameelementsholderhidden");
                break;
        }
       
        errorBox.AddToClassList("errorBoxUp");
    }

    bool IsEmailValid(string email)//verify is the email is in a valid format
    {
        const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|fr)$";//email template
        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }

    IEnumerator OnRegisterButtonClicked()//verify every case on the register tab
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

                if(IsUserExist(newEmailTextField.text, newPasswordTextField.text, 1) == false)
                {
                    LoginInfos newUser = new LoginInfos();//Creation of a new user with an email and a password
                    newUser.emails = newEmailTextField.text;
                    newUser.passwords = newPasswordTextField.text;
                    saveData.data.datas.Add(newUser);//Add user to the list 
                    saveData.SaveToJson();//save the list in Json
                }
                else
                {
                    errorBoxLabel.text = "emails already used";
                    errorBox.RemoveFromClassList("errorBoxUp");
                    yield return new WaitForSeconds(2f);
                    break;
                }
                FramesAnimation();//Launch animations to go to the login tab
                break;
        }
       
        errorBox.AddToClassList("errorBoxUp");
    }

    private bool IsUserExist(string email, string password ,int number)//verify that the email use to register is not already used
    {
        foreach (LoginInfos user in saveData.data.datas)
        {
            if(user.emails == email && number == 1)
            {
                return true;
            }
             if(user.passwords == password && number == 2)
            {
                return true;
            }
        }
        return false;
    }

    void FramesAnimation()//annimate the ui to go from the register tab to the login tab
    {   
        registFrame.AddToClassList("registerWoodFrameHidden");
        transitionFrame.AddToClassList("transitionWoodFrameHidden");
        gear0.AddToClassList("GearRotate");
        gear1.AddToClassList("GearRotate");
        gear2.AddToClassList("GearRotate");
        loginFrame.RemoveFromClassList("loginWoodFramHidden");
    }

    public IEnumerator LogginStatusBox(string text)//show up the disconnect button once connected to photon or hide the button if pressed once connected
    {
        disconnectButton.AddToClassList("disconnectbuttonup");

        if(text == "Disconnected")//if disconnet button is clicked
        {
            disconnectButton.RemoveFromClassList("disconnectbuttonup");//hide button
        }

        loginBoxLabel.text = text;
        loginBox.AddToClassList("logginboxdown");
        yield return new WaitForSeconds(2f);
        loginBox.RemoveFromClassList("logginboxdown");
    }

  [PunRPC]
    public void AddList(string username)//add the username to the list 
    {
        if (!usernames.Contains(username))
        {
            usernames.Add(username);
        }

        UpdateList();
    }

    private void UpdateList()//update the list to add new username 
    {
        list.Clear();
        list.itemsSource = usernames;
        list.makeItem = () => elementList.CloneTree();
        list.bindItem = (element, index) =>
        {
            var label = element.Q<Label>();
            label.text = $"{index + 1}. Account Connected: {usernames[index]}";//display username and number based on index in the list
        };
        list.fixedItemHeight = 60;
        list.Rebuild();
    }

    public void SetupList(string username)//call all connected player and buffer for every new player connecting
    {
        photonView.RPC("AddList", RpcTarget.AllBuffered, username);
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public TMP_InputField createplayerEmail;
    public TMP_InputField createplayerPassword;

    public GameObject playermenu;

    public Text connectText;
    private string URL = "http://localhost:5000/api/player";
    // Start is called before the first frame update
    void Start()
    {
        connectText = GameObject.Find("registerText").GetComponent<Text>();
        if (Singleton.Instance.Token != null) connectText.text = "you are logged in";
        else connectText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Singleton.Instance.Token != null)
        {
            this.gameObject.SetActive(false);
            playermenu.SetActive(true);
        }
    }

    public void usernameInput(string input)
    {
        UserName = input;
        Debug.Log("username : " + input);
    }

    public void passwordInput(string input)
    {
        Password = input;
        Debug.Log("password : " + input);
    }

    void GetToken(string response, long statusCode)
    {

        //connectText.text = token;
        if (statusCode == 201)
        {
            connectText.text = "connection success";
            RestSingleton.Instance._isLoggedIn = true;
            RestSingleton.Instance.token = response;
            Singleton.Instance.Token = response;
            Debug.Log("response :" + response);
            Debug.Log("code :" + statusCode);
        }
        else if (statusCode != 201) { connectText.text = response; }

    }

    public void onRegisterClick()
    {
        UserName = createplayerEmail.text;
        Password = createplayerPassword.text;
        StartCoroutine(RestSingleton.Instance.PostRegisterData(URL, new LoginPlayer(UserName, Password), GetToken));
    }





}

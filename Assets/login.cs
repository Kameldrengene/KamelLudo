using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    private string _username;
    private string _password;
    public TMP_InputField playerEmail;
    public TMP_InputField playerPassword;
    public Text connectText;
    public GameObject playermenu;
    
    // Start is called before the first frame update
    void Start()
    {
        connectText = GameObject.Find("connectText").GetComponent<Text>();
        connectText.text = "";
        //StartCoroutine(RestSingleton.Instance.PostLoginData(URL, new LoginPlayer("butt", "1234"), GetToken));
    }

    // Update is called once per frame
    void Update()
    {
        if (SignalR.Instance.Token!=null)
        {
            this.gameObject.SetActive(false);   
            playermenu.SetActive(true);
        }
        //Debug.Log("value:" + playerEmail.text);
        //Debug.Log("value:" + playerPassword.text);
    }

    void GetToken(string response, long statusCode)
    {

        //connectText.text = token;
        if (statusCode == 200) 
        {
            connectText.text = "connection success";
            RestSingleton.Instance._isLoggedIn = true;
            RestSingleton.Instance.token = response;
            SignalR.Instance.Token = response;
            Debug.Log("response :" + response);
            Debug.Log("code :" + statusCode);
        }
        else if (statusCode == 401) {
            Debug.Log("response: " + response);
            connectText.text = response;
        }
        else
        {
            connectText.text = "Unable to connect, service down";
        }
        
    }

    public void usernameInput()
    {
        _username = playerEmail.text;
        Debug.Log("username : "+ _username);
    }

    public void passwordInput()
    {
        _password = playerPassword.text;
        Debug.Log("password : " + _password);
    }

    public void onLoginClick()
    {
        _username = playerEmail.text;
        _password = playerPassword.text;
        string URL = SignalR.Instance.ConnectionString + "/api/player/authenticate";
        StartCoroutine(RestSingleton.Instance.PostLoginData(URL, new LoginPlayer(_username, _password), GetToken));
    }

}
    
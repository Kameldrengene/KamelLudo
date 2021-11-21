using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    private string _username;
    private string _password;
    public Text connectText;
    public GameObject playermenu;
    private string URL = "http://localhost:5000/api/player/authenticate";
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
        if (Singleton.Instance.Token!=null)
        {
            this.gameObject.SetActive(false);   
            playermenu.SetActive(true);
        }
    }

    void GetToken(string response, long statusCode)
    {

        //connectText.text = token;
        if (statusCode == 200) 
        { 
            connectText.text = "connection success";
            RestSingleton.Instance._isLoggedIn = true;
            RestSingleton.Instance.token = response;
            Singleton.Instance.Token = response;
            Debug.Log("response :" + response);
            Debug.Log("code :" + statusCode);
        }
        else if (statusCode != 200) { connectText.text = response; }
        
    }

    public void usernameInput(string input)
    {
        _username = input;
        Debug.Log("username : "+ input);
    }

    public void passwordInput(string input)
    {
        _password = input;
        Debug.Log("password : " + input);
    }

    public void onLoginClick()
    {
        StartCoroutine(RestSingleton.Instance.PostLoginData(URL, new LoginPlayer(_username, _password), GetToken));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoginPlayer
{
    public LoginPlayer(string Username, string Password)
    {
        username = Username;
        password = Password;
    }

    public string username { get; set; }
    public string password { get; set; }
}

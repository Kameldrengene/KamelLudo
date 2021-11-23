using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestSingleton 
{
    private static readonly object servicelock = new object();
    private static RestSingleton instance = null;
    public bool _isLoggedIn = false;
    public string token = null;
    RestSingleton()
    {
        
    }

    public static RestSingleton Instance
    {
        get
        {
            lock (servicelock)
            {
                if (instance == null)
                {
                    instance = new RestSingleton();
                }
                return instance;
            }
        }
    }

    public IEnumerator PostLoginData(string url, LoginPlayer player, System.Action<string,long> callBack)
    {
        string stringjson = "{" + "\n" + "\"username\"" + ":" + "\""+player.username+"\"" +","+ "\n" + "\"password\"" + ":" + "\""+player.password+"\"" + "\n" + "}";
        string playerJson = JsonUtility.ToJson(new LoginPlayer("butt","1234"));
        Debug.Log("playerjson1 : " + stringjson);
        using (UnityWebRequest www = UnityWebRequest.Post(url, stringjson))
        {
            www.SetRequestHeader("content-type", "application/json");  
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(stringjson));
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }else
            {
                if (www.isDone)
                {
                    Debug.Log(www.result.ToString());
                    long code = www.responseCode;
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    //string token = JsonUtility.FromJson(result);                    
                    Debug.Log("code: "+code);
                    callBack(result,code);
                }
            }
        }

    }
}

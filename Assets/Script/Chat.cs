using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum ModelType{
    GPT3,
    GPT4
}
public class Chat : MonoBehaviour
{
    [Header("Url（API发送地址）")]
    public string apiUrl;
    [Header("API Key")]
    public string key;
    public ModelType modelType = ModelType.GPT3;
    private List<SendData> datalist = new List<SendData>();

    public IEnumerator GetPostData(string postWord, Action<string> action)
    {
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "Post"))
        {
            string setModel = "gpt-3.5-turbo";
            switch(modelType){
                case ModelType.GPT3:
                    setModel = "gpt-3.5-turbo";
                    break;
                case ModelType.GPT4:
                    setModel = "gpt-4";
                    break;
            }
            datalist.Add(new SendData("user", postWord));
            PostData postData = new PostData
            {
                model = setModel,
                messages = datalist,
                stream = "true"
            };
            string _jsonText = JsonUtility.ToJson(postData);
            Debug.Log(_jsonText);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(_jsonText);
            request.uploadHandler =  new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", string.Format("Bearer {0}", key));
            // string n = request.GetRequestHeader("Content-Type");
            // Debug.Log(n);
            // n = request.GetRequestHeader("Authorization");
            // Debug.Log(n);
            request.SendWebRequest();
            // Debug.Log(request.responseCode);
            // Debug.Log(request.isDone);
            //Debug.Log(request.downloadHandler.text);
            //Back back = JsonUtility.FromJson<Back>(request.downloadHandler.text);
            
            while (request.isDone != true)
            {
                Debug.Log(request.isDone);
                yield return null;
                if (request.downloadHandler.text != "")
                {
                    //Debug.Log(request.downloadHandler.text);
                    string msg = request.downloadHandler.text;
                    string[] strs = msg.Split("data:");
                    //List<Dictionary<string, Back>> backList = JsonUtility.FromJson<List<Dictionary<string, Back>>>(msg);
                    string str = "";
                    foreach (var item in strs)
                    {
                        //Debug.Log(item);
                        if (item != "")
                        {
                            Debug.Log(item);
                            Back back = JsonUtility.FromJson<Back>(item);
                            //Debug.Log(back.choices[0].delta.content);
                            if (back.choices[0].delta.content != null)
                            {
                                str += back.choices[0].delta.content;
                            }
                            if (back.choices[0].finish_reason == "stop")
                            {
                                break;
                            }
                        }
                    }
                    action(str);
                    Debug.Log(str);
                    //BackData textBack = JsonUtility.FromJson<BackData>(msg);
                }
                
            }
        }
       
        
    }
}
[Serializable]
public class PostData
{
    public string model;
    public List<SendData> messages;
    public string stream;
}
[Serializable]
public class SendData
{
    public string role;
    public string content;
    public SendData() { }
    public SendData(string _role, string _content)
    {
        role = _role;
        content = _content;
    }
}
[Serializable]
public class BackData
{
    public List<Back> data;
}
[Serializable]
public class Back
{
    public string id;
    public string created;
    public string model;
    public List<MessageBack> choices;
}
[Serializable]
public class MessageBack
{
    public SendData delta;
    public string finish_reason;
    public string index;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("聊天输入框")]
    public InputField input;//聊天输入框
    [Header("发送按钮")]
    public Button sendBtn;//发送按钮
    [Header("对话框")]
    public GameObject[] textBox = new GameObject[2];//聊天框
    [Header("聊天存储框")]
    public Transform infoBox;
    private Chat chat;
    private PreBox nowPreBox;
    private bool isFrist;//是否是第一次返回
    private void Start() {
        sendBtn.onClick.AddListener(Send);
        chat = GetComponent<Chat>();
    }
    
    /// <summary>
    /// 发送
    /// </summary>
    public void Send(){
        string str = input.text;
        StartCoroutine(chat.GetPostData(str, BackBox));
        SendBox(str);
    }

    /// <summary>
    /// 发送聊天框
    /// </summary>
    /// <param name="str">发送的信息</param>
    public void SendBox(string str){
        GameObject gameObj = Instantiate(textBox[0], infoBox);
        gameObj.GetComponent<PreBox>().SetText(str);
        CreateBackBox();
    }

    /// <summary>
    /// 创建回复聊天框
    /// </summary>
    public void CreateBackBox(){
        nowPreBox = Instantiate(textBox[1], infoBox).GetComponent<PreBox>();
        nowPreBox.SetText("请稍等...");
        isFrist = true;
    }

    /// <summary>
    /// 更改回复聊天
    /// </summary>
    /// <param name="str"></param>
    public void BackBox(string str){
        if(isFrist){
            isFrist = false;
            nowPreBox.ClearText();
        }
        nowPreBox.SetText(str);
    }
}

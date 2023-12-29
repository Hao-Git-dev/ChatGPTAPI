using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TexBox : MonoBehaviour
{
    private InputField input;
    private Text showText;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Awake()
    {
        input = GetComponent<InputField>();
        showText = GetComponentInChildren<Text>();
    }
    private void Update() {
        //如果不是只读设为只读
        if(input.readOnly == false){
            input.readOnly = true;
        }
    }

    /// <summary>
    /// 设置文字
    /// </summary>
    /// <param name="str"></param>
    public void SetText(string str){
        showText.text = str;
    }
}

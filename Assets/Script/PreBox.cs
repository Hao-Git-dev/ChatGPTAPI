using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreBox : MonoBehaviour
{
    [Header("输入框")]
    public InputField input;

    public void SetText(string str){
        if(input.text != ""){
            str = str.Replace(input.text, "");
        }
        
        input.text = str;
    }

    public void ClearText(){
        input.text = "";
    }
}

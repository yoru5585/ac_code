using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message_s : MonoBehaviour
{
    [SerializeField, TextArea(0, 5)] string[] Messages;
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update

    public void OnChangeText(int i)
    {
        text.text = Messages[i];
    }

    public void OnMessageEnable(bool exist)
    {
        text.enabled = exist;
    }
}

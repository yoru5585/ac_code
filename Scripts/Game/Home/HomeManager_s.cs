using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager_s : MonoBehaviour
{
    Message_s ms;
    GameManager_s GameManager;
    GameObject clickedObject;
    string clickedObjectName;
    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<Message_s>();
        GameManager = GetComponent<GameManager_s>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.ClickedObjectExist())
        {
            clickedObject = GameManager.GetClickedObject();
            clickedObjectName = clickedObject.name;
            SearchID();
        }
    }

    void SearchID()
    {
        switch (clickedObjectName)
        {
            case "LightSwitches01":
                GetComponent<Light_s>().OnLight();
                clickedObject.GetComponent<AudioSource>().Play();
                break;
            case "Radio":
                clickedObject.GetComponent<Audio_s>().OnPlayMusic();
                break;
            case "Keyboard":
                GetComponent<PC_s>().DisplayON();
                break;
            case "TV-box":
                GetComponent<PC_s>().SwitchCamera(true);
                break;
            case "spMemo_1":
                //Debug.Log("aaaa");
                GameObject tmp = GameObject.Find("MainCanvas").transform.GetChild(5).gameObject;
                tmp.SetActive(true);
                tmp.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
                break;
            default:
                Debug.Log("default");
                break;
        }
    }
}

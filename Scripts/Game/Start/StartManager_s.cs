using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager_s : MonoBehaviour
{
    Message_s ms;
    GameManager_s GameManager;
    Controller_s Controller;
    int taskState = 0;
    GameObject clickedObject;
    string clickedObjectName;
    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<Message_s>();
        GameManager = GetComponent<GameManager_s>();
        Controller = GameObject.Find("DontDestroy").GetComponent<Controller_s>();
    }

    // Update is called once per frame
    void Update()
    {
        //test
        if (Controller.GetDebugDown())
        {
            Debug.Log("DebugMode");
            clickedObjectName = "TV-box";
            taskState = 4;
            CheckTask();
            GetComponent<PC_s>().DisplayON();
            return;
        }

        if (GameManager.ClickedObjectExist())
        {
            clickedObject = GameManager.GetClickedObject();
            clickedObjectName = clickedObject.name;
            SearchID();
            CheckTask();
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

    void CheckTask()
    {
        if (clickedObjectName == null)
        {
            return;
        }

        switch (taskState)
        {
            case 0:
                if (clickedObjectName.Equals("LightSwitches01"))
                {
                    clickedObject.GetComponent<Outline>().enabled = false;
                    GameObject.Find("Radio").GetComponent<Outline>().enabled = true;
                    taskState++;
                    ms.OnChangeText(taskState);
                }
                break;
            case 1:
                if (clickedObjectName.Equals("Radio"))
                {

                    clickedObject.GetComponent<Outline>().enabled = false;
                    GameObject.Find("TomatoPuree_41 Tris").GetComponent<Outline>().enabled = true;
                    taskState++;
                    ms.OnChangeText(taskState);
                }
                break;
            case 2:
                if (clickedObjectName.Equals("TomatoPuree_41 Tris"))
                {
                    clickedObject.GetComponent<Outline>().enabled = false;
                    clickedObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Keyboard").GetComponent<Outline>().enabled = true;
                    taskState++;
                    ms.OnMessageEnable(false);
                }
                break;
            case 3:
                if (clickedObjectName.Equals("Keyboard"))
                {
                    clickedObject.GetComponent<Outline>().enabled = false;
                    GetComponent<PC_s>().DisplayON();
                    taskState++;
                }
                break;
            case 4:
                if (clickedObjectName.Equals("TV-box"))
                {
                    GetComponent<PC_s>().SwitchCamera(true);
                    taskState = 99;
                }
                break;
            default:
                break;
        }

    }
}

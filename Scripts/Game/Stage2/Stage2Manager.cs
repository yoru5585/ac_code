using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stage2Manager : BaseBehaviour_s, IManager_s
{

    GameManager_s GameManager;
    AudioManager_s AudioManager;
    GameObject clickedObject;
    [SerializeField] GameObject wall;
    string clickedObjectName;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameManager = GetComponent<GameManager_s>();
        AudioManager = GetComponent<AudioManager_s>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetRestartDown())
        {
            GameObject.Find("Player").transform.position = Vector3.zero;
        }
        CheckExist();
    }

    public void CheckExist()
    {
        if (GameManager.ClickedObjectExist())
        {
            clickedObject = GameManager.GetClickedObject();
            clickedObjectName = clickedObject.name;
            SearchID();
        }
    }

    public void SearchID()
    {
        switch (clickedObjectName)
        {
            case "Radio":
                AudioManager.OnPlayMainMusic("BGM/Stranger");
                //audiomanager経由でplayerのbgmをチェンジ
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

    public void OnCleared()
    {
        pms.ClearStateFlag[2] = true;
        Transform Player = GameObject.Find("Player").transform;
        Player.parent = pms.transform.parent;
    }

    public void WallOpen()
    {
        wall.SetActive(false);
    }

    public void CloseOpen()
    {
        wall.SetActive(true);
    }

}

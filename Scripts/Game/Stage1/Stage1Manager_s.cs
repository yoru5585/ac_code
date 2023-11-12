using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Manager_s : BaseBehaviour_s, IManager_s
{
    GameManager_s GameManager;
    AudioManager_s AudioManager;
    Transform PlayerTrans;
    GameObject clickedObject;
    string clickedObjectName;
    [SerializeField] GameObject curtain;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject Sihouette;
    [SerializeField] GameObject exit_door;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameManager = GetComponent<GameManager_s>();
        AudioManager = GetComponent<AudioManager_s>();
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetRestartDown())
        {
            PlayerTrans.position = new Vector3(36.64f, 0, -0.017f);
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
                AudioManager.OnPlayMainMusic("BGM/Little More");
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

    public void CurtainOpen()
    {
        curtain.SetActive(false);
    }

    public void WallOpen()
    {
        wall.SetActive(false);
    }

    public void OnColliderGunUsed()
    {
        GetComponent<ColliderGun_s>().enabled = true;
        Sihouette.SetActive(true);
    }

    public void OnExitButtonPushed()
    {
        exit_door.SetActive(false);
        StartCoroutine(Delay_s.DelayMethod(50f, () =>
            {
                Debug.Log("time out");
                exit_door.SetActive(true);
            })
        );
    }

    public void OnClear()
    {
        pms.ClearStateFlag[1] = true;
        Transform Player = GameObject.Find("Player").transform;
        Player.parent = pms.transform.parent;
    }
}

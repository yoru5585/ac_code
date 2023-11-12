using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager_s : BaseBehaviour_s
{
    
    PC_s PC;
    GameObject floppy;
    bool flag = true;//ˆê“x‚Ì‚Ý
    [SerializeField, TextArea(0, 6)] string[] str;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        floppy = GameObject.Find("floppy_disk8");
        PC = GetComponent<PC_s>();
    }

    private void Awake()
    {
        Destroy(GameObject.Find("Player/Main Camera"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetDebugDown())
        {
            FadeManager.Instance.LoadScene("HOmeScene", 0.5f);
            return;
        }

        if (floppy != null)
        {
            return;
        }

        if (flag)
        {
            flag = false;
            GetComponent<SaveManager_s>().Save();
            OnLoad();
            //AudioDestroy();
            //‚È‚º‚©³í‚É“®‚©‚È‚¢
            StartCoroutine(Delay_s.DelayMethod(5f, () =>
                {
                    PC.OnSaveTextData();
                    Destroy(GameObject.Find("Player"));
                    FadeManager.Instance.LoadScene("HomeScene", 0.5f);
                })
            );
        }
        
        
    }

    void OnLoad()
    {
        PC.DisplayON();
        PC.SwitchCamera(true);
        if (pms.ClearState < 3)
        {
            PC.AddText(str[pms.ClearState++]);
        }
        
    }

    void AudioDestroy()
    {
        AudioManager_s audioManager = GetComponent<AudioManager_s>();
        StartCoroutine(audioManager.OnFadeMute(() =>
            {
                PC.OnSaveTextData();
                Destroy(GameObject.Find("Player"));
                FadeManager.Instance.LoadScene("HomeScene", 0.5f);
            })
        );
    }
}

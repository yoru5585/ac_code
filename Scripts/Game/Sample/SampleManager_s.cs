using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SampleManager_s : MonoBehaviour
{
    //enable�Ń��[�h�ύX
    //�V���G�b�g���[�h�ύX�@����@�Ȃ�
    Controller_s Controller;
    ColliderGun_s ColliderGun;
    Pickup_s Pickup;
    bool flag = true;
    [SerializeField]TextMeshProUGUI TMP;
    [SerializeField] GameObject Sihouette;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GameObject.Find("DontDestroy").GetComponent<Controller_s>();
        ColliderGun = GetComponent<ColliderGun_s>();
        Pickup = GetComponent<Pickup_s>();
        //OnChangeMode();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Controller.GetCentralDown())
        //{
        //    Sihouette.SetActive(flag);
        //    flag = !flag;
        //    //OnChangeMode();
        //    //Debug.Log(FileEx.FindPath("Sprites-Mask.mat"));
        //}

        if (Controller.GetDebugDown())
        {
            Debug.Log("qwerty");
            //GetComponent<Twins_s>().SetPlayerColl();
        }
    }

    void OnChangeMode()
    {
        ColliderGun.enabled = flag;
        Pickup.enabled = !flag;
        if (flag)
        {
            TMP.text = "collider�ړ�";
        }
        else
        {
            TMP.text = "���W�I�傫���ω�";
        }
        flag = !flag;
    }

    public void TimeboardTest()
    {
        Debug.Log("start");
        StartCoroutine(Delay_s.DelayMethod(1f, () =>
            {
                Debug.Log("time out");
            })
        );
        
    }
}
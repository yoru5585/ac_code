using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twins_s : ColliderGun_s
{
    //自分のコライダー出し入れ用
    Transform PlayerTwinsCollTrans;
    GameObject Player;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerTwinsCollTrans = GameObject.Find("Player/PlayerTwinsColl").transform;
        PlayerTwinsCollTrans.gameObject.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        Target = PlayerTwinsCollTrans.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectKepperTrans.childCount != 0)
        {
            return;
        }
        //Debug.Log(state);
        if (Controller.GetDebugDown())
        {
            if (state == 0)
            {
                //取り出し
                OnRelease();
                state++;
                return;
            }

            ForcedTransfer();
            return;
        }

        if (Controller.GetActionDown())
        {
            switch (state)
            {
                case 1:
                    //設置
                    OnSet();
                    state++;
                    break;
                case 2:
                    OnRayCheck();
                    if (Target == null)
                    {
                        return;
                    }

                    if (Target.tag == ("Player"))
                    {
                        Target.GetComponent<Dissolve_s>().OnStart();
                        Player.layer = 0;
                        state++;
                    }
                    break;
                default:
                    break;
            }
        }

        switch (state)
        {
            case 1:
                //場所選択
                MovePos();
                break;
            case 3:
                if (Target.GetComponent<Dissolve_s>().GetIsEnd())
                {
                    Target.SetActive(false);
                    OnDissolveEnd();
                    state = 0;
                }
                break;
            default:
                break;

        }

    }
    void OnRelease()
    {
        //レイヤーを変更する
        Target.layer = 2;
        Target.GetComponent<Collider>().isTrigger = true;
        //疑似判定の重力無効
        Target.GetComponent<Rigidbody>().useGravity = false;
        //ついでにy軸の補正（オブジェクトと地面が接するようにするため）
        Target_y_correction = Target.transform.localScale.y / 2;
        //マテリアル変更
        Target.GetComponent<MeshRenderer>().material = clear_mat;
        //outline
        Target.GetComponent<Outline>().enabled = true;

        Target.SetActive(true);

        //プレイヤー
        Player.GetComponent<Rigidbody>().useGravity = false;
        Player.layer = 7;
        Player.tag = "Untagged";
    }

    protected override void OnSet()
    {
        base.OnSet();
        //isTrigger
        Target.GetComponent<Collider>().isTrigger = false;
        //疑似判定の重力有効
        Target.GetComponent<Rigidbody>().useGravity = true;
        //タグ変更
        Target.tag = "Player";
        //親変更
        Target.transform.parent = transform.root.parent;
        //y固定
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        Player.GetComponent<Rigidbody>().freezeRotation = true;
    }

    void OnDissolveEnd()
    {
        //重力
        Player.GetComponent<Rigidbody>().useGravity = true;
        //タグ
        Player.tag = "Player";
        Target.tag = "Untagged";
        //親変更
        Target.transform.parent = Player.transform;
        state = 0;
        //y固定解除
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Player.GetComponent<Rigidbody>().freezeRotation = true;
    }

    void ForcedTransfer()
    {
        Target = PlayerTwinsCollTrans.gameObject;
        OnDissolveEnd();
        Target.SetActive(false);
        state = 0;
    }
}

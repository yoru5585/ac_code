using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twins_s : ColliderGun_s
{
    //�����̃R���C�_�[�o��������ꂽ�肷����
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
                //���o��
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
                    //�ݒu
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
                //�ꏊ�I��
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
        //���C���[��ύX����
        Target.layer = 2;
        Target.GetComponent<Collider>().isTrigger = true;
        //�^������̏d�͖���
        Target.GetComponent<Rigidbody>().useGravity = false;
        //���ł�y���̕␳�i�I�u�W�F�N�g�ƒn�ʂ��ڂ���悤�ɂ��邽�߁j
        Target_y_correction = Target.transform.localScale.y / 2;
        //�}�e���A���ύX
        Target.GetComponent<MeshRenderer>().material = clear_mat;
        //outline
        Target.GetComponent<Outline>().enabled = true;

        Target.SetActive(true);

        //�v���C���[
        Player.GetComponent<Rigidbody>().useGravity = false;
        Player.layer = 7;
        Player.tag = "Untagged";
    }

    protected override void OnSet()
    {
        base.OnSet();
        //isTrigger
        Target.GetComponent<Collider>().isTrigger = false;
        //�^������̏d�͗L��
        Target.GetComponent<Rigidbody>().useGravity = true;
        //�^�O�ύX
        Target.tag = "Player";
        //�e�ύX
        Target.transform.parent = transform.root.parent;
        //y�Œ�
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        Player.GetComponent<Rigidbody>().freezeRotation = true;
    }

    void OnDissolveEnd()
    {
        //�d��
        Player.GetComponent<Rigidbody>().useGravity = true;
        //�^�O
        Player.tag = "Player";
        Target.tag = "Untagged";
        //�e�ύX
        Target.transform.parent = Player.transform;
        state = 0;
        //y�Œ����
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Warp_s : MonoBehaviour
{
    //Player�ɂ���
    int NowRoomNum = 0;
    int NextRoomNum = 0;
    int state = 0; //�������ړ��������ǂ������f����p
    string NowColor = "red";
    string NextColor;
    WarpDestManager_s WarpDestManager;
    Transform PlayerPos;
    [SerializeField] Transform StartTrans;
    [SerializeField] Transform[] loopAry;
    private void Start()
    {
        WarpDestManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<WarpDestManager_s>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "warpwall")
        {
            transform.position = StartTrans.transform.position;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.transform.parent.name != "coll")
        {
            return;
        }

        if (state == 0)
        {
            NowColor = other.name;
            string str = other.transform.parent.transform.parent.name;
            NowRoomNum = int.Parse(str.Remove(0, 4)) - 1;
        }
        
        OnStateChange(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.transform.parent.name != "coll")
        {
            return;
        }

        OnStateChange(other.name);
    }

    void OnMovePlayerTransform()
    {
        //Player�̍��W��room�̃��[�J�����W�ɂ���
        Vector3 target = loopAry[NowRoomNum].transform.InverseTransformPoint(PlayerPos.position);

        //������ړ����room�Ń��[���h���W��
        Vector3 worldPos = loopAry[NextRoomNum].transform.TransformPoint(target);

        //���[���h���W��Player�ɑ��
        PlayerPos.position = worldPos;

        NowRoomNum = NextRoomNum;
        Debug.Log(NextColor + ":"+ NextRoomNum + "�Ƀ��[�v���܂����B");
    }

    void OnStateChange(string name)
    {
        switch (state)
        {
            case 0:
                //������room�ԍ���color��n�������ѐ�������Ă����N���X
                WarpDestManager.GetWarpRoom(NowRoomNum, NowColor);
                NextRoomNum = WarpDestManager.GetRoomNum();
                NextColor = WarpDestManager.GetColor();

                //��ѐ悪����΂������ state++
                //�Ȃ����state�ω����Ȃ�
                if (NextRoomNum == 99)
                {
                    return;
                }
                state++;
                break;
            case 1:
                //�V����������color����ѐ��color�Ɠ����ł����state++;
                //�������state������
                if (NextColor.Equals(name))
                {
                    state++;
                }
                else
                {
                    state = 0;
                }

                break;
            case 2:
                //�����o����color���������܂ł���room�Ɠ����ł����state++;
                //�������state--;
                if (NowColor.Equals(name))
                {
                    state++;
                }
                else
                {
                    state--;
                }

                break;
            case 3:
                //�����o����color���V����������color�Ɠ����ł����state++;
                //���[�v����
                //�������state--;(�����Ԃ����ꍇ)
                if (NextColor.Equals(name))
                {
                    state = 0;
                    OnMovePlayerTransform();
                }
                else
                {
                    state--;
                }

                break;
            default:
                break;
        }
    }
}
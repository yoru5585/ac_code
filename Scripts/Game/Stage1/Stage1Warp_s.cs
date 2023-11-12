using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Warp_s : MonoBehaviour
{
    //Playerにつける
    int NowRoomNum = 0;
    int NextRoomNum = 0;
    int state = 0; //部屋を移動したかどうか判断する用
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
        //Playerの座標をroomのローカル座標にする
        Vector3 target = loopAry[NowRoomNum].transform.InverseTransformPoint(PlayerPos.position);

        //それを移動先のroomでワールド座標化
        Vector3 worldPos = loopAry[NextRoomNum].transform.TransformPoint(target);

        //ワールド座標をPlayerに代入
        PlayerPos.position = worldPos;

        NowRoomNum = NextRoomNum;
        Debug.Log(NextColor + ":"+ NextRoomNum + "にワープしました。");
    }

    void OnStateChange(string name)
    {
        switch (state)
        {
            case 0:
                //今いるroom番号とcolorを渡したら飛び先を教えてくれるクラス
                WarpDestManager.GetWarpRoom(NowRoomNum, NowColor);
                NextRoomNum = WarpDestManager.GetRoomNum();
                NextColor = WarpDestManager.GetColor();

                //飛び先があればそれを代入 state++
                //なければstate変化しない
                if (NextRoomNum == 99)
                {
                    return;
                }
                state++;
                break;
            case 1:
                //新しく入ったcolorが飛び先のcolorと同じであればstate++;
                //違ったらstate初期化
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
                //抜け出したcolorがさっきまでいたroomと同じであればstate++;
                //違ったらstate--;
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
                //抜け出したcolorが新しく入ったcolorと同じであればstate++;
                //ワープ処理
                //違ったらstate--;(引き返した場合)
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

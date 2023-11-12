using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGun_s : BaseBehaviour_s
{
    [HideInInspector] public GameObject Target;
    public Material wire_mat;
    public Material clear_mat;
    Transform OriginTrans;
    public Transform ObjectKepperTrans;
    [HideInInspector] public float Target_y_correction;
    [HideInInspector] public int state;

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetActionDown())
        {
            switch (state)
            {
                case 0:
                    OnRayCheck();
                    if (Target == null)
                    {
                        return;
                    }

                    if (Target.gameObject.tag == "collgun")
                    {
                        //最初に通る
                        Target.GetComponent<Dissolve_s>().OnStart();
                        state++;
                    }
                    break;
                case 2:
                    //取り出す
                    OnRelease();
                    state++;
                    break;
                case 3:
                    //設置時
                    OnSet();
                    state = 0;
                    break;
                default:
                    break;

            }
        }

        switch (state)
        {
            case 1:
                if (Target.GetComponent<Dissolve_s>().GetIsEnd())
                {
                    //dissolveが終わったらhold
                    OnHold();
                    state++;
                }
                break;
            case 3:
                //どこに設置するか決める
                MovePos();
                break;
            default:
                break;

        }
        
    }

    public void OnRayCheck()
    {
        Target = null;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 3, false);

        if (Physics.Raycast(ray, out hit))
        {
            Target = hit.collider.gameObject;
            Debug.Log(Target.name);
        }

        
    }

    void OnHold()
    {
        //取得したColliderをObjectKeeperに移動させる
        OriginTrans = Target.transform.parent;
        Target.transform.parent = ObjectKepperTrans;

    }

    void OnRelease()
    {
        //ObjectKeeperに保管したObjectを取り出す
        Target.transform.parent = OriginTrans;
        //Rayの判定を消すためcolliderをfalseにする
        //Target.GetComponent<Collider>().enabled = false;
        //→ではなくレイヤーを変更する
        Target.layer = 2;
        //ついでにy軸の補正（オブジェクトと地面が接するようにするため）
        Target_y_correction = Target.transform.localScale.y / 2;
        //マテリアル変更
        Target.GetComponent<MeshRenderer>().material = clear_mat;
        //outline
        Target.GetComponent<Outline>().enabled = true;
    }

    protected virtual void OnSet()
    {
        //オブジェクト解放時
        Target.GetComponent<MeshRenderer>().material = wire_mat;
        Target.GetComponent<Outline>().enabled = false;
        Target.layer = 0;
        //Target.GetComponent<Collider>().enabled = true;
    }

    public void MovePos()
    {
        GameObject RayObject = null;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3);

        if (Physics.Raycast(ray, out hit))
        {
            RayObject = hit.collider.gameObject;
        }

        //Debug.Log(RayObject);

        if (RayObject == null)
        {
            //Target.transform.position = new Vector3(ray.direction.x * 10, ray.direction.y * 10 + Target_y_correction, ray.direction.z * 10);
            return;
        }

        Target.transform.position = new Vector3(hit.point.x, hit.point.y + Target_y_correction, hit.point.z);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_s : BaseBehaviour_s
{
    string clickedObjectName;
    GameObject clickedObject;
    GameObject Sihouette;
    GameStop_s GameStop;
    bool IsScope;
    bool flag; //âΩìxÇ‡é¿çsÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈÇΩÇﬂ
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameStop = GetComponent<GameStop_s>();
        Sihouette = GameObject.Find("Player/Main Camera").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (pms.IsStop)
        {
            return;
        }

        if (Controller.GetActionDown())
        {
            flag = false;
            OnRayCheck();
        }

        if (Controller.GetEscapeDown())
        {
            GameStop.OnGameStop();
        }

        if (Controller.GetCentralDown())
        {
            IsScope = !IsScope;
            OnColliderGunUsed(IsScope);
        }
    }

    void OnRayCheck()
    {
        clickedObjectName = null;
        clickedObject = null;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 3, false);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < 4)
            {
                clickedObjectName = hit.collider.gameObject.name;
                clickedObject = hit.collider.gameObject;
            }
        }

        Debug.Log(clickedObjectName);
    }

    public bool ClickedObjectExist()
    {
        if (clickedObject == null || flag) { return false; }
        return true;
    }

    public GameObject GetClickedObject()
    {
        flag = true;
        return clickedObject;
    }

    public void OnColliderGunUsed(bool flag = true)
    {
        GetComponent<ColliderGun_s>().enabled = flag;
        Sihouette.SetActive(flag);
    }
}

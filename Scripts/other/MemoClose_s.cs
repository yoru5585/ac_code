using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoClose_s : MonoBehaviour
{
    GameObject parent;
    parameterManager_s pms;
    Image[] memo;
    int index;
    void Start()
    {
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
        parent = transform.parent.gameObject;
        memo = new Image[parent.transform.childCount-1];
        for (int i = 0; i < memo.Length; i++)
        {
            memo[i] = parent.transform.GetChild(i).gameObject.GetComponent<Image>();
            //Debug.Log(memo[i].name);
        }
        transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(NextMemo);
        transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(EndMemo);
    }

    public void EndMemo()
    {
        foreach (Image obj in memo)
        {
            obj.enabled = false;
        }
        index = 0;
        parent.SetActive(false);
    }

    public void NextMemo()
    {
        if (pms.ClearState == 0)
        {
            return;
        }

        memo[index].enabled = false;
        index++;
        if (index > pms.ClearState)
        {
            index = 0;
        }
        memo[index].enabled = true;

        //memo[index].enabled = false;
        //while (true)
        //{
        //    index++;

        //    if (index > pms.ClearStateFlag.Length)
        //    {
        //        index = 0;
        //        break;
        //    }

        //    if (pms.ClearStateFlag[index-1])
        //    {
        //        //index++;
        //        break;
        //    }

        //}
        //memo[index].enabled = true;
    }
}

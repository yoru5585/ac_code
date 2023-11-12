using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDes_s : MonoBehaviour
{
    //ゲーム全体で影響するオブジェクトを管理するスクリプト
    //シングルトン
    public static DontDes_s instance;

    private void Awake()
    {
        //自身が重複しているかチェック
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

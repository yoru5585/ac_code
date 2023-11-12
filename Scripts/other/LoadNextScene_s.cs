using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadNextScene_s : MonoBehaviour
{
    parameterManager_s pms;
    // Start is called before the first frame update
    void Start()
    {
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
        //シーン破棄完了されると実行
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    void SceneUnloaded(Scene thisScene)
    {
        //メッセージの別シーンへの移動で使おうと思ったが無理だった
    }

    
}

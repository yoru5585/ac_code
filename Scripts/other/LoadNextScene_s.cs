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
        //�V�[���j�����������Ǝ��s
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    void SceneUnloaded(Scene thisScene)
    {
        //���b�Z�[�W�̕ʃV�[���ւ̈ړ��Ŏg�����Ǝv����������������
    }

    
}
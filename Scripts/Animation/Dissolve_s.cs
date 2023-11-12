using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve_s : MonoBehaviour
{
    [SerializeField] Material DissolveMat;
    [SerializeField] Material IniMat;
    readonly int ShaderPropertyID_Threshold = Shader.PropertyToID("_Threshold");
    bool flag = false;
    bool IsEnd = false;
    float value = 0;
    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            value += Time.deltaTime;
            if (value >= 1)
            {
                flag = false;
                IsEnd = true;
                value = 0;
                return;
            }
            DissolveMat.SetFloat(ShaderPropertyID_Threshold, value);
        }   
    }

    public void OnStart()
    {
        //溶解スタート
        GetComponent<MeshRenderer>().material = DissolveMat;
        IsEnd = false;
        flag = true;
    }

    public bool GetIsEnd()
    {
        return IsEnd;
    }

    void Init()
    {
        //編集用マテリアルからゲーム用マテリアルに変更
        GetComponent<MeshRenderer>().material = IniMat;
    }
}

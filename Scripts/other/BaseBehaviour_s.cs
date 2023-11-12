using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour_s : MonoBehaviour
{
    [HideInInspector] public parameterManager_s pms;
    [HideInInspector] public Controller_s Controller;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
        Controller = GameObject.Find("DontDestroy").GetComponent<Controller_s>();
    }
}

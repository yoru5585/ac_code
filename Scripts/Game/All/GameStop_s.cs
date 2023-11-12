using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStop_s : MonoBehaviour
{
    Transform Canvas;
    parameterManager_s pms;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("MainCanvas").transform;
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStop()
    {
        Canvas.GetChild(4).gameObject.SetActive(false);
        Canvas.GetChild(2).gameObject.SetActive(true);
        GetComponent<Option_s>().SetIndex(2);
        Cursor.visible = true;
        pms.IsStop = true;
    }

    public void OnExitButtonClicked()
    {
        Canvas.GetChild(4).gameObject.SetActive(true);
        Canvas.GetChild(2).gameObject.SetActive(false);
        Cursor.visible = pms.IsMouseEnable;
        pms.IsStop = false;
    }

    public void OnOptionButtonClicked()
    {
        Canvas.transform.GetChild(2).gameObject.SetActive(false);
        Canvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    
}

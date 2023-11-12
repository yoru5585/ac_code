using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_s : MonoBehaviour
{
    [SerializeField] GameObject PCDisplay;
    [SerializeField] Text Text;
    [SerializeField] InputField InputText;
    [SerializeField] Camera PCCamera;
    [SerializeField] Camera Main;
    [SerializeField] Scrollbar Scrollbar;
    parameterManager_s pms;
    PCResult_s PCResult;

    bool DelayEndFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
        PCResult = GetComponent<PCResult_s>();

        if (!pms.PCtextData.Equals(""))
        {
            OnLoadTextData();
            Scrollbar.value = pms.ScrollbarValue;
        }
    }

    void Update()
    {
        if (PCDisplay.activeSelf)
        {
            if (InputText.isFocused != true)
            {
                InputText.ActivateInputField();
            }
        }
    }

    public void DisplayON()
    {
        PCDisplay.SetActive(true);
        InputText.text = "";
    }

    public void DisplayOFF()
    {
        PCDisplay.SetActive(false);
    }

    public void SwitchCamera(bool flag)
    {
        if (!PCDisplay.activeSelf)
        {
            return;
        }
        pms.IsStop = flag;
        PCCamera.enabled = flag;
        Main.enabled = !flag;
    }

    public void OnSaveTextData()
    {
        pms.PCtextData = Text.text;
    }

    public void OnLoadTextData()
    {
        Text.text = pms.PCtextData;
    }

    public bool DelayEnd_flag
    {
        set
        {
            DelayEndFlag = value;
        }
        get
        {
            return DelayEndFlag;
        }
        
    }

    public void AddText(string str)
    {
        if (str.Equals(""))
        {
            Text.text += "\n" + InputText.text;
            PCResult.SearchAddress(InputText.text, Delay());
            InputText.text = "";
            return;
        }

        Text.text += "\n" + str;
    }

    IEnumerator Delay(float second = 0.5f)
    {
        DelayEndFlag = false;

        Text.text += "\n" + "<<wait";
        for (int i = 0; i < 3; i++)
        {
            Text.text += ". ";
            yield return new WaitForSeconds(second);
        }

        for (int i = 0; i < 4; i++)
        {
            Text.text += ". ";
            yield return new WaitForSeconds(0.02f);
        }

        DelayEndFlag = true;
    }
}

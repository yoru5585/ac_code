using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PCResult_s : MonoBehaviour
{
    PC_s PC;
    parameterManager_s pms;
    [SerializeField] Text Text;
    [SerializeField] Scrollbar Scrollbar;
    string HelpMess =
        "[>>tutorial] -move to tutorial stage" +
        "\n[>>loop] -move to loop" +
        "\n[>>tower] -move to tower" +
        "\n[>>home] -move to home" +
        "\n[>>exit] -close to PC";
    string StageMess =
        "Please note nausea and headache.";
    bool SearchFlag = false;
    string MethodName = null;
    string NextSceneName;
    bool IsDebug;
    // Start is called before the first frame update
    void Start()
    {
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
        PC = GetComponent<PC_s>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SearchFlag)
        {
            //相手の終了を待つ
            if (PC.DelayEnd_flag)
            {
                PC.DelayEnd_flag = false;
                SearchFlag = false;
                Invoke(MethodName, 0);
                return;
            }
        }
        
    }

    public void SearchAddress(string str, IEnumerator enumerator)
    {
        switch (str)
        {
            case ">>help":
                StartCoroutine(enumerator);
                MethodName = "HelpResult";
                SearchFlag = true;
                break;
            case ">>tutorial":
                StartCoroutine(enumerator);
                NextSceneName = "TutorialScene"; //ここにステージのScene名
                MethodName = "StageResult";　//メソッド名
                SearchFlag = true;
                break;
            case ">>loop":
                if (pms.IsTutorialCleared != true) 
                {
                    PC.AddText("<< ! Please complete the tutorial first !");
                    break;
                }

                StartCoroutine(enumerator);
                NextSceneName = "Stage1Scene"; //ここにステージのScene名
                MethodName = "StageResult";　//メソッド名
                SearchFlag = true;
                break;
            case ">>tower":
                if (pms.IsTutorialCleared != true)
                {
                    PC.AddText("<< ! Please complete the tutorial first !");
                    break;
                }

                StartCoroutine(enumerator);
                NextSceneName = "Stage2Scene";
                MethodName = "StageResult";
                SearchFlag = true;
                break;
            case ">>home":
                StartCoroutine(enumerator);
                NextSceneName = "HomeScene";
                MethodName = "HomeResult";
                SearchFlag = true;
                break;
            case ">>exit":
                ExitResult();
                break;
            case ">>zangoose":
                PC.AddText("<<my best partner :)");
                break;
            case ">>debug":
                PC.AddText("<<DebugMode ON");
                IsDebug = true;
                break;
            default:
                if (IsDebug)
                {
                    switch (str)
                    {
                        case "/c tutorial":
                            PC.AddText("<<completed.");
                            pms.ClearState++;
                            pms.IsTutorialCleared = true;
                            pms.ClearStateFlag[0] = true;
                            break;
                        case "/c loop":
                            PC.AddText("<<completed.");
                            pms.ClearState++;
                            pms.ClearStateFlag[1] = true;
                            break;
                        case "/c tower":
                            PC.AddText("<<completed.");
                            pms.ClearState++;
                            pms.ClearStateFlag[2] = true;
                            break;
                        case "/r state":
                            PC.AddText("<<completed.");
                            pms.ClearState = 0;
                            break;
                        case "/e debug":
                            PC.AddText("<<DebugMode OFF");
                            IsDebug = false;
                            break;
                        case "/s":
                            PC.AddText("<<completed.");
                            GetComponent<SaveManager_s>().Save();
                            break;
                        case "/l":
                            PC.AddText("<<completed.");
                            GetComponent<SaveManager_s>().Load();
                            break;
                        case "/d save":
                            PC.AddText("<<completed.");
                            GetComponent<SaveManager_s>().DeleteData();
                            break;
                        case "/e game":
                            #if UNITY_EDITOR
                                UnityEditor.EditorApplication.isPlaying = false;
                            #else
                                Application.Quit();
                            #endif
                            break;
                    }
                    return;
                }
                NotFound();
                break;
        }
    }

    void HelpResult()
    {
        PC.AddText(HelpMess);
    }

    void HomeResult()
    {
        if (NextSceneName == null)
        {
            PC.AddText("<<unknown error");
            return;
        }

        PC.AddText(StageMess);
        StartCoroutine(MoveDelay());
    }

    void StageResult()
    {
        if (NextSceneName == null)
        {
            PC.AddText("<<unknown error");
            return;
        }

        PC.AddText(StageMess);
        StartCoroutine(MoveDelay());
    }

    void ExitResult()
    {
        PC.AddText("<<completed.");
        PC.SwitchCamera(false);
        PC.DisplayOFF();
        Cursor.visible = pms.IsMouseEnable;
    }

    void NotFound()
    {
        PC.AddText("<<this command is error.");
    }

    IEnumerator MoveDelay()
    {
        string str = "\n" + "[__________]";
        float speed = 0.5f;
        Text.text += str;

        for (int i = 0; i < 10; i++)
        {
            if (i == 6)
            {
                speed = 0.05f;
            }
            //最後の行を削除
            Text.text = Text.text.Remove(Text.text.LastIndexOf("\n"));
            //@を挿入
            str = str.Remove(str.IndexOf("_"), 1);
            str = str.Insert(2, "■");
            //行を挿入
            Text.text += str;

            yield return new WaitForSeconds(speed);
        }

        PC.AddText("Movement completed.");
        PC.OnSaveTextData();
        pms.ScrollbarValue = Scrollbar.value;
        SceneManager.LoadScene(NextSceneName);
    }
}

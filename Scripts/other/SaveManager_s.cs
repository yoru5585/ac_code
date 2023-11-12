using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager_s : MonoBehaviour
{
    string filePath;
    SaveData_s save;
    parameterManager_s pms;

    private void Awake()
    {
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        Debug.Log(filePath);
        save = new SaveData_s();
        save.StageClearFlag = new bool[3];
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<SaveData_s>(data);

            GetData();
            Debug.Log("ロードしました。");
            return;
        }

        Debug.Log("データがないのでロードできませんでした。");
    }

    public void Save()
    {
        SetData();
        string json = JsonUtility.ToJson(save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        Debug.Log("セーブしました。");
    }

    public void SetData()
    {
        save.StageClearFlag[0] = pms.ClearStateFlag[0];
        save.StageClearFlag[1] = pms.ClearStateFlag[1];
        save.StageClearFlag[2] = pms.ClearStateFlag[2];
        save.IsTutorialCleared = pms.IsTutorialCleared;
        save.ClearState = pms.ClearState;
    }

    public void GetData()
    {
        pms.ClearStateFlag[0] = save.StageClearFlag[0];
        pms.ClearStateFlag[1] = save.StageClearFlag[1];
        pms.ClearStateFlag[2] = save.StageClearFlag[2];
        pms.IsTutorialCleared = save.IsTutorialCleared;
        pms.ClearState = save.ClearState;
    }

    public void DeleteData()
    {
        File.Delete(filePath);
    }
}


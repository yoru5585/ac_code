using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parameterManager_s : MonoBehaviour
{
    //option
    public bool IsStop;
    public bool IsCameraReverse;
    public bool IsMouseEnable;
    public Vector2 rotationSpeed;
    public float WalkSpeed;
    public float Mastervol;
    public float ScrollbarValue;
    public bool IsMoveReverse;
    public bool IsTutorialCleared;

    //ÉVÅ[ÉìéÛÇØìnÇµ
    public bool[] ClearStateFlag;
    public int ClearState;
    [TextArea(0,6)]public string PCtextData;

    void Start()
    {
        ClearStateFlag = new bool[3];
    }
}

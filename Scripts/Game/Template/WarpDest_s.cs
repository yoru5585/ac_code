using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WarpDest_s
{
    int Link; //Ÿ‚Ìƒ[ƒvæ
    int RoomNum;
    string Color;

    public WarpDest_s(int RoomNum, string Color, int index)
    {
        this.RoomNum = RoomNum;
        this.Color = Color;
        Link = index;
    }

    public string GetColor()
    {
        return Color;
    }

    public int GetRoomNum()
    {
        return RoomNum;
    }

    public int GetNext()
    {
        return Link;
    }
}

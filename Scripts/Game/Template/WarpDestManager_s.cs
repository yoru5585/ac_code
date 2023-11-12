using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDestManager_s : MonoBehaviour
{
    //WarpDestä«óùóp
    //linkÇÕîzóÒÇÃóvëfêî
    WarpDest_s[] Warps =
    {
        new WarpDest_s(0, "sky", 3),
        new WarpDest_s(0, "blue", 2),
        new WarpDest_s(1, "sky", 1),
        new WarpDest_s(1, "blue", 0),
        new WarpDest_s(1, "sky_2", 6),
        new WarpDest_s(1, "purple", 7),
        new WarpDest_s(2, "purple", 9),
        new WarpDest_s(2, "sky_2", 8),
        new WarpDest_s(3, "purple", 7),
        new WarpDest_s(3, "sky_2", 6)
    };

    int rtnNum;
    string rtnColor;

    void Start()
    {
        
        
    }

    public void GetWarpRoom(int room, string color)
    {
        rtnNum = 99;
        rtnColor = "";

        foreach (WarpDest_s wd in Warps)
        {
            if (!wd.GetColor().Equals(color))
            {
                continue;
            }

            if (wd.GetRoomNum() != room)
            {
                continue;
            }
            else
            {
                SearchNext(wd);
            }
        }
    }

    void SearchNext(WarpDest_s wd)
    {
        rtnNum = Warps[wd.GetNext()].GetRoomNum();
        rtnColor = Warps[wd.GetNext()].GetColor();
    }

    public int GetRoomNum()
    {
        return rtnNum;
    }

    public string GetColor()
    {
        return rtnColor;
    }
}

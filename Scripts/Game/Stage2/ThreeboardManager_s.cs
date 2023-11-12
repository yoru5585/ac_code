using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeboardManager_s : MonoBehaviour
{
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (count >= 3)
        {
            GetComponent<Stage2Manager>().WallOpen();
        }
        else
        {
            GetComponent<Stage2Manager>().CloseOpen();
        }
    }

    public void countup()
    {
        count++;
    }

    public void countdown()
    {
        count--;
    }
}

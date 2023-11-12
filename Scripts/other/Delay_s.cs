using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Delay_s
{
    public static IEnumerator DelayMethod(float waitTime, Action action)
    {
        Debug.Log("delay comp");
        yield return new WaitForSeconds(waitTime);
        action();
    }
}

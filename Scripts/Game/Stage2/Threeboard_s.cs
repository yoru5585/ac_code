using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threeboard_s : Switchboard_s
{
    bool IsOff;
    protected override void Update()
    {
        base.Update();
        if (IsOff)
        {
            IsOff = false;
            EndEvent();
        }
    }

    protected override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
        foreach (string str in coll_tag)
        {
            if (collision.gameObject.tag.Contains(str))
            {
                IsOff = true;
                break;
            }
        }
    }

    public void EndEvent()
    {
        End?.Invoke();
        Debug.Log("end-three");
    }
}

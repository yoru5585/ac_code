using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroy_s : MonoBehaviour
{
    //animation�I������destroy
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}

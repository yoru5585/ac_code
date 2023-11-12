using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clearboard_s : MonoBehaviour
{
    //フロッピーディスクを入手してhomeに戻るためのボード
    //の前にloading
    // Start is called before the first frame update

    public UnityEvent function;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (function != null)
            {
                FireEvent();
            }
            
            FadeManager.Instance.LoadScene("LoadingScene", 1.0f);
            Debug.Log("enter");
        }
    }

    public void FireEvent()
    {
        //event call
        function?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clearboard_s : MonoBehaviour
{
    //�t���b�s�[�f�B�X�N����肵��home�ɖ߂邽�߂̃{�[�h
    //�̑O��loading
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
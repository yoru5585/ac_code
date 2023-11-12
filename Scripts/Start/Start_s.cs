using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_s : MonoBehaviour
{
    //タイトルボタン系
    GameObject player;
    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("MainCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        player.GetComponent<Animator>().applyRootMotion = true;
        player.GetComponent<Animator>().SetTrigger("start");
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(true);
        canvas.transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(Delay_s.DelayMethod(3f, () =>
            {
                player.GetComponent<BoxCollider>().isTrigger = false;
                player.GetComponent<Rigidbody>().useGravity = true;
            })
        );

        //Cursor.visible = false;
    }

    public void OnOptionButtonClicked()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<Option_s>().SetIndex(0);
    }

    public void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

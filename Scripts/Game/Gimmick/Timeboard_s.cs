using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timeboard_s : MonoBehaviour
{
    bool flag;
    Controller_s Controller;
    MeshRenderer insideMat; 
    [SerializeField]Material green;
    [SerializeField]Material red;
    public UnityEvent function;
    // Start is called before the first frame update
    void Start()
    {
        insideMat = transform.parent.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        Controller = GameObject.Find("DontDestroy").GetComponent<Controller_s>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (Controller.GetActionDown())
            {
                insideMat.material = green;
                insideMat.gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(Delay_s.DelayMethod(0.5f, () =>
                    {
                        insideMat.material = red;
                    })
                );
                FireEvent();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = false;
        }
    }

    public void FireEvent()
    {
        //event call
        function?.Invoke();
    }
}

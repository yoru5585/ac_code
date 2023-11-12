using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWarp_s : MonoBehaviour
{
    [SerializeField] Transform TargetWarp;
    Transform PlayerTrans;
    public int count = 0;
    private void Start()
    {
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (count >= 2)
        {
            count = 0;
            TargetWarp.gameObject.GetComponent<SimpleWarp_s>().count = -2;
            OnChangePos();
        }
    }

    void OnChangePos()
    {
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerTrans.position = TargetWarp.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            count++;
        }
    }
}

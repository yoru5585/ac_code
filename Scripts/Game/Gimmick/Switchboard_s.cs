using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switchboard_s : MonoBehaviour
{
    MeshRenderer mesh;
    bool IsOn;
    public string [] coll_tag;
    [SerializeField] Material green;
    [SerializeField] Material red;
    public UnityEvent Enter;
    public UnityEvent End;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsOn)
        {
            IsOn = false;
            FireEvent();
        }
    }

    public void FireEvent()
    {
        //event call
        Enter?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (string str in coll_tag)
        {
            if (collision.gameObject.tag.Contains(str))
            {
                mesh.material = green;
                IsOn = true;
                Debug.Log("enter");
                break;
            }
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        foreach (string str in coll_tag)
        {
            if (collision.gameObject.tag.Contains(str))
            {
                mesh.material = red;
                IsOn = false;
                Debug.Log("exit");
                break;
            }
        }
    }

    public bool GetIsOn()
    {
        return IsOn;
    }
}

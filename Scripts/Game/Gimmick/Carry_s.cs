using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry_s : MonoBehaviour
{
    GameObject clickedObject;
    Transform TargetParent;
    Transform MyParent;
    Controller_s Controller;
    bool IsPickup = false;
    Vector2 v2;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GameObject.Find("DontDestroy").GetComponent<Controller_s>();
        TargetParent = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {

        //Cursor.visible = false;
        if (Controller.GetActionDown())
        {
            if (IsPickup)
            {
                IsPickup = false;
                OnRelease();
            }
            else
            {
                OnRayCheck();
                OnPickup();
            }
        }
    }

    void OnRayCheck()
    {
        clickedObject = null;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 3, false);

        if (Physics.Raycast(ray, out hit))
        {
            clickedObject = hit.collider.gameObject;
        }
        
    }

    void OnPickup()
    {
        if (clickedObject == null)
        {
            return;
        }

        if (clickedObject.tag != "item")
        {
            return;
        }

        IsPickup = true;
        MyParent = clickedObject.transform.parent;
        clickedObject.transform.parent = TargetParent;
        float distance = Vector3.Distance(clickedObject.transform.position, Camera.main.transform.position);
        clickedObject.transform.localPosition = new Vector3(0, 0, distance);
        clickedObject.transform.localRotation = new Quaternion(0, 180, 0, 1);
        clickedObject.GetComponent<BoxCollider>().isTrigger = true;
        clickedObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnRelease()
    {
        clickedObject.transform.parent = MyParent;
        clickedObject.GetComponent<BoxCollider>().isTrigger = false;
        clickedObject.GetComponent<Rigidbody>().isKinematic = false;
    }

}

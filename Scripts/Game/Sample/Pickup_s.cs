using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pickup_s : MonoBehaviour
{
    string clickedObjectName;
    GameObject clickedObjeck;
    GameObject Player;
    Transform TargetParent;
    Transform MyParent;
    Controller_s Controller;
    bool IsPickup = false;
    Vector2 v2;
    [SerializeField] AudioMixer AudioMixer;
    [SerializeField] AudioReverbZone AudioReverbZone;
    [SerializeField] AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GameObject.Find("DontDestroy").GetComponent<Controller_s>();
        TargetParent = Camera.main.transform;
        Player = GameObject.Find("Player");
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
                ChangeAudioVol();
            }
            else
            {
                OnRayCheck();
                OnPickup();
            }
        }

        if (IsPickup)
        {
            MovePos();
        }
    }

    void OnRayCheck()
    {
        clickedObjectName = null;
        clickedObjeck = null;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 3, false);

        if (Physics.Raycast(ray, out hit))
        {
            clickedObjectName = hit.collider.gameObject.name;
            clickedObjeck = hit.collider.gameObject;
        }

        Debug.Log(clickedObjectName);
    }

    void OnPickup()
    {
        if (clickedObjectName != "Radio")
        {
            return;
        }

        IsPickup = true;
        MyParent = clickedObjeck.transform.parent;
        clickedObjeck.transform.parent = TargetParent;
        float distance = Vector3.Distance(clickedObjeck.transform.position, Camera.main.transform.position);
        clickedObjeck.transform.localPosition = new Vector3(0, 0, distance);
        clickedObjeck.transform.localRotation = new Quaternion(0, 180, 0, 1);
        clickedObjeck.GetComponent<BoxCollider>().enabled = false;
        clickedObjeck.GetComponent<Rigidbody>().useGravity = false;
        clickedObjeck.GetComponent<Rigidbody>().freezeRotation = true;
        ScaleCalc();
    }

    void OnRelease()
    {
        clickedObjeck.transform.parent = MyParent;
        clickedObjeck.GetComponent<BoxCollider>().enabled = true;
        clickedObjeck.GetComponent<Rigidbody>().useGravity = true;
        clickedObjeck.GetComponent<Rigidbody>().freezeRotation = false;
    }

    void OnChangeScale()
    {
        Transform MyPos = clickedObjeck.transform;
        float Distance = Vector3.Distance(MyPos.position, Camera.main.transform.position);
        float Scale = v2.x * Distance + v2.y;
        //Debug.Log(Scale);
        MyPos.transform.localScale = new Vector3(Scale, Scale, Scale);
    }

    void ScaleCalc()
    {
        GameObject dis1 = clickedObjeck;
        float dis1_scale = dis1.transform.localScale.x;
        float dis1_distance = Vector3.Distance(dis1.transform.position, Camera.main.transform.position);
        Vector2 t = Vector2.zero;

        t.x = dis1_scale / dis1_distance;
        t.y = dis1_scale - (t.x * dis1_distance);

        v2 = t;

    }

    void MovePos()
    {
        GameObject RayObject = null;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(ray.origin, ray.direction * 80, Color.blue, 3);

        if (Physics.Raycast(ray, out hit))
        {
            RayObject = hit.collider.gameObject;
        }

        Debug.Log(RayObject);

        if (RayObject == null)
        {
            clickedObjeck.transform.position = new Vector3(ray.direction.x * 80, ray.direction.y * 80, ray.direction.z * 80);
            OnChangeScale();
            return;
        }

        clickedObjeck.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        OnChangeScale();
        //Debug.Log(hit.point.z);
    }

    void ChangeAudioVol()
    {
        float scale = clickedObjeck.transform.localScale.x;
        float dis = 42.5f * scale - 27.5f;
        if (scale > 10.0f)
        {
            AudioSource.spatialBlend = 0;
        }
        else
        {
            AudioSource.spatialBlend = 1.0f;
        }

        if (scale > 5.0f)
        {
            AudioReverbZone.enabled = true;
        }
        else
        {
            AudioReverbZone.enabled = false;
        }

        if (dis > 5000)
        {
            dis = 5000;
        }
        else if (dis < 2)
        {
            dis = 2;
        }

        float vol;
        if (scale < 1)
        {
            vol = 25 * scale - 25.0f;
        }
        else
        {
            vol = scale - 1.0f;
            if (vol > 5.0f)
            {
                vol = 5.0f;
            }
        }

        AudioMixer.SetFloat("BGMvol", vol);
        AudioSource.maxDistance = dis;
    }
}

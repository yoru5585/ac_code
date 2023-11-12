using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaseColl_s : MonoBehaviour
{
    //‘½•ªŽg‚Á‚Ä‚È‚¢
    [SerializeField] Transform[] Array;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (index < Array.Length)
        {
            Debug.Log(index);
            OnReplace();
            index++;
        }
    }

    void OnReplace()
    {
        Vector3 pos = Vector3.zero;
        Vector3 scale = Vector3.zero;
        Transform parent = Array[index].parent;
        string name = Array[index].name;
        Array[index].position = pos;
        Array[index].localScale = scale;

        GameObject tmp = Instantiate((GameObject)Resources.Load("Collider"),parent);
        tmp.name = name;
        tmp.transform.position = pos;
        tmp.transform.localScale = scale;

        Destroy(Array[index].gameObject);
    }
}

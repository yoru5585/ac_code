using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_s : MonoBehaviour
{
    Transform light_t;
    bool sw = false;
    // Start is called before the first frame update
    void Start()
    {
        light_t = GameObject.Find("Lights").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLight()
    {
        sw = !sw;
        for (int i = 0; i < 5; i++)
        {
            light_t.GetChild(i).gameObject.SetActive(sw);
        }
    }
}

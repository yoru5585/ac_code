using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWarp_s : MonoBehaviour
{

    Transform PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "warpwall_0")
        {
            transform.position = new Vector3(-52.02f, 0, -0.12f);
        }

        if (collision.gameObject.name == "warpwall_2")
        {
            transform.position = new Vector3(-80f, 0, 0.12f);
        }

        
    }

}

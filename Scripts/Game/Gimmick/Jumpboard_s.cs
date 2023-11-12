using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpboard_s : MonoBehaviour
{
    [SerializeField] float jump_value = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jump_value, 0), ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DesideSound_s : MonoBehaviour
{
    [SerializeField] AudioMixer AudioMixer;
    AudioSource SEAudio;
    // Start is called before the first frame update
    void Start()
    {
        SEAudio = GameObject.Find("Player/SE").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

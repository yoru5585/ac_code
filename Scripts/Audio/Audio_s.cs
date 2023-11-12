using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_s : MonoBehaviour
{
    AudioSource Audio;
    AudioClip[] Audio_data;
    [SerializeField] AudioMixer AudioMixer;
    Transform Player;
    int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        Audio_data = Resources.LoadAll<AudioClip>("BGM");
        Audio = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayMusic()
    {
        if (state == Audio_data.Length)
        {
            state = 0;
            Audio.Stop();
            //AudioMixer.SetFloat("Envol", -10);
            return;
        }

        Audio.clip = Audio_data[state++];
        Audio.Play();
        //AudioMixer.SetFloat("Envol", -40);
    }

    public float GetAudioValue(Vector3 playerPosition)
    {
        var distance = Vector3.Distance(transform.position, playerPosition);
        var AnimationCurve = new AnimationCurve();
        return AnimationCurve.Evaluate(distance / Audio.maxDistance);
    }

    public void OnStopMusic()
    {
        state = 3;
        OnPlayMusic();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager_s : BaseBehaviour_s
{
    [SerializeField] AudioMixer AudioMixer;
    AudioSource MainAudio;
    AudioSource SEAudio;
    bool IsMusicOn;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MainAudio = GameObject.Find("Player/BGM").GetComponent<AudioSource>();
        SEAudio = GameObject.Find("Player/SE").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlaySE(string pass)
    {
        if (pass.Equals("")) { pass = "SE/select"; }
        SEAudio.PlayOneShot((AudioClip)Resources.Load(pass));
    }

    public void OnPlayMainMusic(string pass)
    {
        if (IsMusicOn)
        {
            IsMusicOn = false;
            MainAudio.Stop();
            MainAudio.clip = (AudioClip)Resources.Load("teion");
            MainAudio.outputAudioMixerGroup = AudioMixer.FindMatchingGroups("Environment")[0];
            return;
        }

        IsMusicOn = true;
        MainAudio.clip = (AudioClip)Resources.Load(pass);
        MainAudio.outputAudioMixerGroup = AudioMixer.FindMatchingGroups("BGM")[0];
        MainAudio.Play();
    }

    public void OnChangeMasterVol()
    {
        AudioMixer.SetFloat("Mastervol", pms.Mastervol);
    }

    public IEnumerator OnFadeMute(Action action, float speed = 0.05f)
    {
        for (int i = 100; i > 0; i--)
        {
            //Debug.Log(MainAudio.volume);
            MainAudio.volume = (float)i / 100;
            yield return new WaitForSeconds(speed);
        }

        Debug.Log("audioend");
        action();
    }
}

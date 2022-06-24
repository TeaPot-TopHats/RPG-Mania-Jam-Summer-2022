using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        instance = this;

        foreach( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            /*
             * If you want to add more mixer groups add a new enum in Sounds and add it here. 
             */
            switch (s.audioType)
            {
                case Sound.AudioTypes.soundEffects:
                    s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }
            if (s.playOnAwake)
                s.source.Play();
        }// end of foreach

    }// end of void Awake

    public void Play(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if( s == null)
        {
            Debug.LogError("sound:" + clipname + "does NOT exist!");
            return;
        }
        s.source.Play();
    }// end of void play
    public void Stop(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("sound:" + clipname + "does NOT exist!");
            return;
        }
        s.source.Stop();
    }// end of void stop

    public void updateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effect Volume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
    }

}// end of class


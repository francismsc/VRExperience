using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicSounds, sfxSounds, narrationSounds;
    [SerializeField] private AudioSource musicSource, sfxSource, narrationSource;
    [SerializeField] private AudioSource stepsSource;

    //HardCoded needs to be properly done
    [SerializeField] private AudioClip[] heartbeats;
    [SerializeField] private AudioClip[] breathing;
    [SerializeField] private AudioSource heartbeatSource, breathingSource;
    [SerializeField] Animation doorAnimation;

    private Queue<AudioClip> narrationQueue = new Queue<AudioClip>();
    private bool isNarrationPlaying = false;

    private void Start()
    {
        AudioTrigger.OnAudioTrigger += StepsGetCloser;
        ClaustrophobiaLvl.OnLevelChange += ClaustrophobiaSound;
    }

    private void OnDisable()
    {
        AudioTrigger.OnAudioTrigger -= StepsGetCloser;
        ClaustrophobiaLvl.OnLevelChange -= ClaustrophobiaSound;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one AudioManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlayNarration(string name)
    {
        Sound s = Array.Find(narrationSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Narration sound not found: " + name);
            return;
        }

        narrationQueue.Enqueue(s.clip);

        if (!isNarrationPlaying)
        {
            StartCoroutine(PlayNarrationQueue());
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    private void StepsGetCloser()
    {
        stepsSource.volume += 0.05f;
        stepsSource.spatialBlend -= 0.05f;
    }

    private IEnumerator PlayNarrationQueue()
    {
        isNarrationPlaying = true;

        while (narrationQueue.Count > 0)
        {
            AudioClip clip = narrationQueue.Dequeue();
            narrationSource.clip = clip;
            narrationSource.Play();
            yield return new WaitForSeconds(clip.length);
        }

        isNarrationPlaying = false;
    }

    private void ClaustrophobiaSound(int currentLevel)
    {
        switch(currentLevel)
        {
            case 3:
                heartbeatSource.clip = heartbeats[0];           
                breathingSource.clip = breathing[0];
                PlayHeartBeatSound();
                PlayBreathingSound();
                break;
            case 5:
                heartbeatSource.clip = heartbeats[1];
                breathingSource.clip = breathing[1];
                PlayHeartBeatSound();
                PlayBreathingSound();
           
                break;
            case 6:
                PlaySFX("Knock");
                break;
            case 7:
                heartbeatSource.clip = heartbeats[2];
                breathingSource.clip = breathing[2];
                PlayHeartBeatSound();
                PlayBreathingSound();
                PlaySFX("DoorCreak");
                doorAnimation.Play();

                break;


        }


    }

    private void PlayHeartBeatSound()
    {
        heartbeatSource.loop = true;
        heartbeatSource.Play();
    }

    private void PlayBreathingSound()
    {
        breathingSource.loop = true;
        breathingSource.Play();
    }

}

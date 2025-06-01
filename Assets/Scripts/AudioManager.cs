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

    private Queue<AudioClip> narrationQueue = new Queue<AudioClip>();
    private bool isNarrationPlaying = false;

    private void Start()
    {
        AudioTrigger.OnAudioTrigger += StepsGetCloser;
    }

    private void OnDisable()
    {
        AudioTrigger.OnAudioTrigger -= StepsGetCloser;
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
        stepsSource.volume += 0.075f;
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

}

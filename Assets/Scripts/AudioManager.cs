using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    [SerializeField] private AudioSource stepsSource;

    private void Start()
    {
        AudioTrigger.OnTrigger += StepsGetCloser;
    }

    private void OnDisable()
    {
        AudioTrigger.OnTrigger -= StepsGetCloser;
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

    public void PlaySfx(string name)
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
}

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class PostProcessingLevelTween : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private PostProcessingLevelData[] levelData;

    private FilmGrain filmGrain;
    private Vignette vignette;
    private Bloom bloom;

    private Coroutine currentTween;

    private void Awake()
    {
        volume.profile.TryGet(out filmGrain);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out bloom);
    }

    private void OnEnable()
    {
        ClaustrophobiaLvl.OnLevelChange += HandleLevelChange;
    }

    private void OnDisable()
    {
        ClaustrophobiaLvl.OnLevelChange -= HandleLevelChange;
    }

    private void HandleLevelChange(int newLevel)
    {
        if (newLevel < 0) 
        {
            newLevel = 0;
        }

        if(newLevel >= levelData.Length)
        {
            newLevel = levelData.Length-1;
        }

        if (currentTween != null)
            StopCoroutine(currentTween);

        currentTween = StartCoroutine(TweenToLevel(levelData[newLevel]));
    }

    private IEnumerator TweenToLevel(PostProcessingLevelData target)
    {
        float time = 0f;

        float startGrain = filmGrain.intensity.value;
        float startVignette = vignette.intensity.value;
        float startBloom = bloom.intensity.value;

        filmGrain.type.value = target.filmGrainType;

        while (time < target.transitionTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / target.transitionTime);

            filmGrain.intensity.value = Mathf.Lerp(startGrain, target.filmGrainIntensity, t);
            vignette.intensity.value = Mathf.Lerp(startVignette, target.vignetteIntensity, t);
            bloom.intensity.value = Mathf.Lerp(startBloom, target.bloomIntensity, t);

            yield return null;
        }

        currentTween = null;
    }
}
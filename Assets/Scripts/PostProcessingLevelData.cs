using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "PostProcessingLevelData", menuName = "PostProcessing/LevelData", order = 1)]
public class PostProcessingLevelData : ScriptableObject
{
    [Range(0, 1)]
    public float filmGrainIntensity;
    public FilmGrainLookup filmGrainType;


    [Range(0, 1)]
    public float vignetteIntensity;

    [Range(0f, 10f)]
    public float bloomIntensity;

    public float transitionTime = 3f;
}

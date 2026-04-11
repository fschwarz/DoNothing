using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DopamineVFXManager : MonoBehaviour
{
    private static readonly int BaseLevel = Shader.PropertyToID("_BaseLevel");

    [Range(0f,1f)]
    public float dopamine;

    public Renderer dopamineBar;
    public Volume postProcessingVolume;
    public AnimationCurve saturationCurve;
    private ColorAdjustments colorAdjustments;
    public Transform hand;
    public float shakingFrequency = 1;
    public float shakingIntensity = 1;
    void Start()
    {
        postProcessingVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
    }

    void Update()
    {
        colorAdjustments.saturation.value = saturationCurve.Evaluate(dopamine)*200-100;
        dopamineBar.material.SetFloat(BaseLevel,dopamine);
        hand.localPosition = new Vector3(Mathf.PerlinNoise(Time.time*shakingFrequency+2,dopamine+1.2125f),Mathf.PerlinNoise(Time.time*shakingFrequency,dopamine)) * (dopamine * 3 * shakingIntensity);
        hand.rotation = Quaternion.Euler(new Vector3(hand.rotation.eulerAngles.x,hand.rotation.eulerAngles.y,Mathf.PerlinNoise(Time.time*dopamine*0.01f,dopamine)));
    }
}

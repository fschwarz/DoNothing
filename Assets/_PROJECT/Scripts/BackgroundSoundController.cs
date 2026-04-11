using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundSoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource defaultSource;

    public AudioSource goodSource;

    public AudioSource badSource;

    public AudioSource breathingSource;

    void Update()
    {
        float dopamine = GameManager.Instance.Dopamine;
        
        if (dopamine >= 0.5)
            goodSource.volume = (dopamine - 0.5f) * 2;
        else
            goodSource.volume = 0f;

        if (dopamine < 0.5)
        {
            float nextValue = (1 - (dopamine * 2));
            badSource.volume = nextValue * 0.2f; // 0-20% volume
            breathingSource.volume = nextValue;
            defaultSource.volume = dopamine * 2;
        }
        else
        {
            badSource.volume = 0f;
            breathingSource.volume = 0f;
            defaultSource.volume = 1f;
        }
    }
}
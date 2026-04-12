using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Global State")]

    [Range(0f, 1f)]
    public float Dopamine = 1f;

    public float DopamineDrainEquilibrium = -0.2f;

    public float DopamineDrainVelocity = -0.2f;

    public float TotalTime = 0f;

    public int Points = 0;

    [Header("Sounds")]
    public AudioClip SwipeSound;

    public AudioClip PleasedSound;

    private int ContentCounter = 0;

    private float ContentStartTime = 0f;

    private int ContentLikeCount = 0;

    private AudioSource AudioSource;

    private float LikeRewardValue = 0.05f;

    public void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(PointsReward), 0, 0.5f);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    public void LateUpdate()
    {
        TotalTime = Time.timeSinceLevelLoad;

        int freshnessTimeInSeconds = 4;
        float contentWatchTimeinSeconds = TotalTime - ContentStartTime;
        float stalenessModifier = Math.Max(contentWatchTimeinSeconds - freshnessTimeInSeconds, 0) * 0.05f;

        float nextValue = Dopamine + DopamineDrainVelocity * Time.deltaTime;

        float drainDelta = Math.Abs(DopamineDrainVelocity - DopamineDrainEquilibrium);

        if (DopamineDrainVelocity > DopamineDrainEquilibrium)
        {
            DopamineDrainVelocity -= drainDelta * Time.deltaTime;
        }
        else if (DopamineDrainVelocity < DopamineDrainEquilibrium)
        {
            DopamineDrainVelocity += drainDelta * Time.deltaTime;
        }

        // We don't want to lower dopamine in the first few seconds, so people have time to learn the mechanic
        if (nextValue <= 0f || nextValue >= 1f || (TotalTime < 5 && DopamineDrainVelocity < 0))
            return;

        Dopamine = nextValue;
    }

    private void PointsReward()
    {
        if (Dopamine < 0.95f)
        {
            return;
        }

        Points += 10;
    }

    public void LikeReward()
    {
        // Debug.Log("LIKE");
        int likesAllowed = 7;

        DopamineDrainVelocity += LikeRewardValue - (ContentLikeCount * (LikeRewardValue / likesAllowed));
        ContentLikeCount++;
    }

    public void SwipeReward()
    {
        float swipeDelta = TotalTime - ContentStartTime;
        float swipeReward = swipeDelta >= 2 ? 0.5f : -LikeRewardValue;
        DopamineDrainVelocity += swipeReward;

        // Audio volume starts decreasing at 50% dopamine
        AudioSource.volume = Math.Min(GameManager.Instance.Dopamine * 2, 1);
        AudioSource.PlayOneShot(SwipeSound);
        if (swipeReward > 0)
            StartCoroutine(PlaySoundWithDelay(PleasedSound, 0.5f));

        ContentLikeCount = 0;
        ContentCounter++;
        ContentStartTime = TotalTime;
    }

    private IEnumerator PlaySoundWithDelay(AudioClip sound, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        AudioSource.PlayOneShot(sound);
    }
}

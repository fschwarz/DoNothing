using System;
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

    private int ContentCounter = 0;

    private float ContentStartTime = 0f;

    private int ContentLikeCount = 0;

    public void Start()
    {
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

        if (nextValue <= 0f || nextValue >= 1f)
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
        Debug.Log("LIKE");
        float defaultLikeReward = 0.1f;
        int likesAllowed = 6;

        DopamineDrainVelocity += defaultLikeReward - (ContentLikeCount * (defaultLikeReward / likesAllowed));
        ContentLikeCount++;
    }

    public void SwipeReward()
    {
        float swipeDelta = TotalTime - ContentStartTime;
        float swipeReward = swipeDelta >= 2 ? 0.4f : 0;
        DopamineDrainVelocity += swipeReward;

        ContentLikeCount = 0;
        ContentCounter++;
        ContentStartTime = TotalTime;
    }
}

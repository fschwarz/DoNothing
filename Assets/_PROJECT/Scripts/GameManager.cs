using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the global access point
    public static GameManager Instance { get; private set; }

    [Header("Global State")]
    public int Dopamine = 500;

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
}

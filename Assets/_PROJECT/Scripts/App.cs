using System;
using UnityEngine;

public class App : MonoBehaviour
{
    public void Start()
    {
        ScreenInteractionHandler.Instance.OnSwipe += Swipe;
        ScreenInteractionHandler.Instance.OnTap += Tap;
    }

    public virtual void Tap(Vector2 tapPosition)
    {
        
    }

    public virtual void Swipe(Vector2 swipeInteraction)
    {
        
    }
}

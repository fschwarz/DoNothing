using System;
using UnityEngine;

public class App : MonoBehaviour
{
    public void Start()
    {
        ScreenInteractionHandler.Instance.OnSwipeFinished += SwipeFinished;
        ScreenInteractionHandler.Instance.OnTap += Tap;
        ScreenInteractionHandler.Instance.OnSwipingInProgress += Swiping;
    }

    public virtual void Swiping(Vector2 swipeProgress)
    {
        
    }

    public virtual void Tap(Vector2 tapPosition)
    {
        
    }

    public virtual void SwipeFinished(Vector2 swipeInteraction)
    {
        
    }
}

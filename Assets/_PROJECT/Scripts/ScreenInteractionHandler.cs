using System;
using _PROJECT.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenInteractionHandler : MonoBehaviour
{
    public Vector2 startSwipePos;
    public Vector2 stopSwipePos;
    public bool isSwipe;
    public event Action<Vector2> OnSwipeFinished;
    Camera cam;
    public event Action<Vector2> OnTap;
    public event Action<Vector2> OnSwipingInProgress;
    public float swipeDeadzone = 2f;
    public static ScreenInteractionHandler Instance;

    public void Awake()
    {
        cam = Camera.main;
        Instance = this;
    }

    public void Update()
    {
        Vector2 position = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(position,Vector2.zero);
            if(raycastHit2D.collider)
                startSwipePos = raycastHit2D.point;
            isSwipe = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            stopSwipePos = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if ((stopSwipePos - startSwipePos).magnitude < swipeDeadzone)
            {
                OnTap?.Invoke(startSwipePos);
            }
            else
                OnSwipeFinished?.Invoke(stopSwipePos-startSwipePos);
            Debug.DrawLine(startSwipePos, stopSwipePos);
            isSwipe = false;
        }

        if (isSwipe)
        {
            Vector2 pos = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            OnSwipingInProgress?.Invoke(pos - startSwipePos);
        }
    }
}

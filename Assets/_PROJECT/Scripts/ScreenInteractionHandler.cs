using System;
using _PROJECT.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenInteractionHandler : MonoBehaviour
{
    public Vector2 startSwipePos;
    public Vector2 stopSwipePos;
    public bool isSwipe;
    public event Action<Vector2> OnSwipe;
    Camera cam;
    public event Action<Vector2> OnTap;
    public float swipeDeadzone = 0.5f;
    public LayerMask layerMask;
    public static ScreenInteractionHandler Instance;

    public void Awake()
    {
        cam = Camera.main;
        Instance = this;
    }

    public void Update()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(position,Vector2.zero);
            if(raycastHit2D.collider != null)
                startSwipePos = raycastHit2D.point;
        }
        else if (Input.GetMouseButtonUp(0))
        {
                stopSwipePos = transform.InverseTransformPoint(Input.mousePosition);
            if ((stopSwipePos - startSwipePos).magnitude < swipeDeadzone)
            {
                OnTap?.Invoke(startSwipePos);
            }
            else
                OnSwipe?.Invoke(stopSwipePos-startSwipePos);
            Debug.DrawLine(startSwipePos, stopSwipePos);
            isSwipe = false;
        }
    }
}

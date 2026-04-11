using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float hour = 0;
    public float minute = 0;

    public float speed = 2;
    public float hourPointerRotation = 0;
    public float minutePointerRotation = 0;
    public Transform hourPointer;
    public Transform minutePointer;
    
    public void Update()
    {
        minute += speed * Time.deltaTime;
        if (minute > 59)
        {
            hour += Mathf.FloorToInt(minute / 60);
            minute %= 60;
        }

        hour %= 24;
        hourPointerRotation = hour / 12f * 360;
        minutePointerRotation = minute / 60f * 360;
        hourPointer.transform.rotation = Quaternion.Euler(0, 0, -hourPointerRotation);
        minutePointer.transform.rotation = Quaternion.Euler(0, 0, -minutePointerRotation);
    }
}

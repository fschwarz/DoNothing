using System;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public void Update()
    {
        Cursor.visible = false;
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

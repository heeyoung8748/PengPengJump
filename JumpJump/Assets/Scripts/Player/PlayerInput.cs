using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string RotateAxisName = "Horizontal";
    public float RotateDirection { get; private set; }

    void Update()
    {
        RotateDirection = Input.GetAxis(RotateAxisName);
    }
}

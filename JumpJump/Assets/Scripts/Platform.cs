using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool IsOnClamped = false;
    public UnityEvent IsClamped = new UnityEvent();

    public void OnEnable()
    {
        IsOnClamped = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.activeInHierarchy == true && IsOnClamped == false)
        {
            IsOnClamped = true;
            Debug.Log("소환간다");
            IsClamped.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboZone : MonoBehaviour
{
    private Platform _platform;
    private void Start()
    {
        _platform = GetComponentInParent<Platform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(_platform.IsOnClamped == false && other.tag == "Player")
        {
            _platform.IsOnClamped = true;
            GameManager.Instance.ComboIncrease();
            _platform.IsClamped.Invoke();
        }
    }
}

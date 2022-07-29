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

    public void Update()
    {
        if (gameObject.activeSelf == true && transform.position.y < 0f &&IsOnClamped == false)
        {
            StartCoroutine("FloatingPlatform");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.activeInHierarchy == true && IsOnClamped == false)
        {
            IsOnClamped = true;
            GameManager.Instance.AddCurrentScore();
            IsClamped.Invoke();
        }
    }

    IEnumerator FloatingPlatform()
    {
        Vector3 startPos = transform.position;
        while(startPos.y < 0f)
        {
            yield return new WaitForSeconds(0.02f);
            startPos.y += 0.5f;
            transform.position = startPos;
        }
        startPos.y = 0f;
        transform.position = startPos;
        
    }
}

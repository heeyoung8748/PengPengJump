using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _buttonPressedTime = 0.0f;
    private float _buttonReleasedTime = 0.0f;
    public int MaxJumpDegree = 30;
    private bool _isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();


    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine("buttonPressSec");
        }
        else if(Input.GetKeyUp(KeyCode.Space) && _isJumping == false)
        {
            StartCoroutine("Jump");
        }    
    }
    IEnumerator buttonPressSec()
    {
        while(_isJumping==false && _buttonPressedTime < 20f)
        {
            yield return new WaitForSeconds(0.03f);
            _buttonPressedTime += 0.05f;
        }
    }
    IEnumerator Jump()
    {
        _isJumping = true;
        Vector3 pos = transform.position;
        Debug.Log($"버튼 누른 시간: {_buttonPressedTime}");
        while(pos.y < 10 && _buttonPressedTime > 0)
        {
            yield return new WaitForSeconds(0.002f);
            pos.y += _buttonPressedTime * Time.deltaTime;
            _buttonPressedTime -= 0.1f;
            transform.position = pos;
        }
        _buttonPressedTime = 0.0f;
        _isJumping = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _isJumping = false;
        }
    }
}

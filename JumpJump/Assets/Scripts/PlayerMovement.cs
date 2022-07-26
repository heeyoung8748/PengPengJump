using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    public int MaxJumpDegree = 20;
    public float RotateSpeed = 20f;
    private float _buttonPressedTime = 0.0f;
    private bool _isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine("buttonPressSec");
        }
        else if(Input.GetKeyUp(KeyCode.Space) && _isJumping == false)
        {
            Jump();
        }    
        rotate();
    }

    private void rotate()
    {
        float rotationAmount = _input.RotateDirection * RotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }

    IEnumerator buttonPressSec()
    {
        while(_isJumping==false && _buttonPressedTime < MaxJumpDegree)
        {
            yield return new WaitForSeconds(0.03f);
            _buttonPressedTime += 0.1f;
        }
    }
    void Jump()
    {
        if(_isJumping)
        {
            return;
        }
        _isJumping = true;
        Vector3 pos = transform.position;
        Debug.Log($"버튼 누른 시간: {_buttonPressedTime}");
        _rigidbody.AddForce(0, 50f, _buttonPressedTime);
        /*
          while(pos.y < 10 && _buttonPressedTime > 0)
         {
             yield return new WaitForSeconds(0.003f);
             pos.y += _buttonPressedTime * Time.deltaTime;
             _buttonPressedTime -= 0.1f;
             transform.position = pos;
         }
         */

        _buttonPressedTime = 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _isJumping = false;
        }
    }
}

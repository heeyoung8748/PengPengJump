using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    public float RotateSpeed = 20f;
    private float _maxJumpDegree = 30f;
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
        Time.timeScale = 2;
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
        while(_isJumping==false && _buttonPressedTime < _maxJumpDegree)
        {
            yield return new WaitForSeconds(0.03f);
            _buttonPressedTime += 0.05f;
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
        Debug.Log($"��ư ���� �ð�: {_buttonPressedTime}");
        _rigidbody.AddForce(0, 80f, _buttonPressedTime);

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
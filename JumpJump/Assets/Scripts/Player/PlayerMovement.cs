using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    public int JumpSpeed = 5;
    private float _buttonPressedTime = 0.0f;
    private bool _isJumping = false;
    private bool _isReadyToJump = false;
    private GameObject _ground;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //_input = GetComponent<PlayerInput>();
    }
    void Update()
    {
        if(_isJumping == false)
        {
            if (Input.GetKey(KeyCode.Space) && _isReadyToJump == false)
            {
                _isReadyToJump = true;
                StartCoroutine("buttonPressSec");
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                StartCoroutine("Jump");
                StartCoroutine("Tumble");
                _isReadyToJump = false;
            }
        }
    }

    IEnumerator buttonPressSec()
    {
        while(_isReadyToJump)
        {
            yield return new WaitForSeconds(0.1f);
            _buttonPressedTime += 0.2f;
            Vector3 groundPos = _ground.transform.position;
            groundPos.y -= 0.1f;
            _ground.transform.position = groundPos;

        }
    }


    IEnumerator Jump()
    {
        if(_isJumping)
        {
            yield break;
        }
        _isJumping = true;
        Debug.Log($"버튼 누른 시간: {_buttonPressedTime}");
        _rigidbody.AddForce(0, 400f, 0);
        while(_buttonPressedTime > 0)
        {
            _rigidbody.AddForce(transform.forward * _buttonPressedTime * JumpSpeed * Time.deltaTime);
            _buttonPressedTime -= Time.deltaTime;
        }
        _buttonPressedTime = 0.0f;
    }

    private float _deltaAngle = 360 / Mathf.PI;
    private float _elapsedTime = 0f;
    private Vector3 _initPosition;
    IEnumerator Tumble()
    {
        _elapsedTime = 0f;
        _initPosition = transform.forward;

        while (_elapsedTime < Mathf.PI)
        {
            _elapsedTime += Time.deltaTime;

            Quaternion deltaQuaternion = Quaternion.Euler(_deltaAngle * Time.deltaTime, 0f, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaQuaternion);

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isJumping = false;
        if(collision.transform.tag == "Ground" || collision.transform.tag == "Combo")
        {
            _ground = collision.gameObject;
        }
        else
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground" && other.GetComponent<Platform>().IsOnClamped == false && _isJumping == false)
        {
            Vector3 lookPos = other.transform.position;
            lookPos.y = 0;
            transform.LookAt(lookPos);

        }
    }
    void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.WasItComboBefore = false;
    }
}

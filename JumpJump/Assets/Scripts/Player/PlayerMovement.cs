using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    public int JumpSpeed = 5;
    //private float _maxJumpDegree = 50f;
    private float _buttonPressedTime = 0.0f;
    private bool _isJumping = false;
    private bool _isReadyToJump = false;
    private GameObject _ground;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
    }
    // Update is called once per frame
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
                StartCoroutine("Tumble");
                StartCoroutine("Jump");
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
        _rigidbody.AddForce(0, 600f, 0);
        while(_buttonPressedTime > 0)
        {
            _rigidbody.AddForce(transform.forward * _buttonPressedTime * JumpSpeed * Time.deltaTime);
            _buttonPressedTime -= Time.deltaTime;
        }
        _buttonPressedTime = 0.0f;
    }

    private float _deltaAngle = 360 / 4; // 점프 시간 동안 360도를 회전해야 한다.
    private float _elapsedTime = 0f;
    private Vector3 _initPosition;
    IEnumerator Tumble()
    {
        _elapsedTime = 0f;
        _initPosition = transform.forward;

        while (_elapsedTime < 4)
        {
            _elapsedTime += Time.deltaTime;

            //Vector3 deltaPosition = new Vector3(0f, 40f * Mathf.Sin(_elapsedTime), _elapsedTime * _buttonPressedTime);
            //_rigidbody.MovePosition(_initPosition + deltaPosition);
            Quaternion deltaQuaternion = Quaternion.Euler(_deltaAngle * Time.deltaTime, 0f, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaQuaternion);

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "DeadZone" || transform.position.y < -0.5)
        {
            Die();
        }
        else if(collision.transform.tag == "Ground")
        {
            _isJumping = false;
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
    }
}

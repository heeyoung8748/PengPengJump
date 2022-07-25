using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _jumpdegree = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();    
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _jumpdegree >= 3)
        {
            _jumpdegree += 0.3f;
        }
        else
        {
            _rigidbody.AddForce(Vector3.up * _jumpdegree, ForceMode.Impulse);
        }
    }
}

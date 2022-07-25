using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public int MoveSpeed = 3;
    public float _jumpdegree = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();


    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _jumpdegree <= 250)
        {
            _jumpdegree += 30f;
            Debug.Log("now jumpdegree : {_jumpdegree}");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _jumpdegree);
        }
    }
}

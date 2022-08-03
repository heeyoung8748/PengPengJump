using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChange : MonoBehaviour
{
    private float _elapsedTime;
    private TextMeshProUGUI _startText;
    private bool _isBlink;
    private void Start()
    {
        _startText = GetComponentInChildren<TextMeshProUGUI>();
        _isBlink = true;
    }
    void Update()
    {
        
        if (Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene("Play");
            GameManager.Instance.WasItComboBefore = false;
        }
        TextBlink();
    }

    void TextBlink()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= 1f)
        {
            _elapsedTime = 0f;
            _isBlink = !_isBlink;
            return;
        }
        if(_isBlink)
        {
            _startText.text = "Press L to start";
        }
        else
        {
            _startText.text = " ";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    public TextMeshProUGUI _bestTimeUI;
    void Awake()
    {
        _bestTimeUI = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        GameManager.Instance.OnHighScoreChanged.AddListener(UpdateText);
    }

    public void UpdateText(int highScore)
    {
        Debug.Log($"�ְ�����: {highScore}");
        _bestTimeUI.text = $"{highScore}";
    }
}

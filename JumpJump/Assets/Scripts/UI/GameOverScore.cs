using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        GameManager.Instance.OnScoreChanged.AddListener(UpdateText);
    }

    void UpdateText(int score)
    {
        Debug.Log($"{score}Á¡");
        _ui.text = $"{score}";
    }
}

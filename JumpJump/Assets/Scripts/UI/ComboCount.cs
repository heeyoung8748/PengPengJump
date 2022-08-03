using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboCount : MonoBehaviour
{
    public TextMeshProUGUI _ui;
    void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        GameManager.Instance.IsCombo.AddListener(UpdateText);
    }

    public void UpdateText(int comboCount)
    {
        _ui.text = $"{comboCount}";
    }
}

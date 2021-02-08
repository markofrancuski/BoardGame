﻿using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ManaUIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _manaLeftText;

    private void Start()
    {
        if (!_manaLeftText)
        {
            _manaLeftText = GetComponent<TextMeshProUGUI>();
        }
        UpdateMana(GameManager.MaxMana);
    }

    private void OnDisable()
    {
        GameManager.OnManaChanged -= UpdateMana;
    }

    private void OnEnable()
    {
        GameManager.OnManaChanged += UpdateMana;
    }

    private void UpdateMana(int amount)
    {
        _manaLeftText.text = amount.ToString();
    }

}

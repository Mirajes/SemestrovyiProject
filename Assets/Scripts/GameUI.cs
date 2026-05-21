using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _sanityBar;

    public static Action<float, float> UpdateSanityBar;

    private void OnEnable()
    {
        UpdateSanityBar += UpdateSanityBar;
    }

    private void OnDisable()
    {
        UpdateSanityBar -= UpdateSanityBar;
    }

    private void OnUpdateSanityBar(float maxValue, float currentValue)
    {
        _sanityBar.fillAmount = currentValue / maxValue;
    }
}

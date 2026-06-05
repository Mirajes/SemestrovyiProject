using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;
    [SerializeField] private Transform _healthFill;

    public static Action Spawn;
    public static Action<float, float> BarChange;

    private void OnEnable()
    {
        Spawn += OnSpawn;
        BarChange += OnBarChange;
    }

    private void OnDisable()
    {
        Spawn -= OnSpawn;
        BarChange -= OnBarChange;
    }

    private void OnSpawn()
    {
        _healthBar.gameObject.SetActive(true);
    }

    private void OnBarChange(float current, float max)
    {
        var scale = _healthFill.localScale;

        float value = current / max;
        _healthFill.localScale = new Vector3(value, scale.y, scale.z);

        if (value <= 0f)
            _healthBar.gameObject.SetActive(false);
    }
}
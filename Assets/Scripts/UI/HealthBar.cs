using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;
    [SerializeField] private Transform _healthFill;

    public void Spawn()
    {
        _healthBar.gameObject.SetActive(true);
    }

    public void UpdateBar(float current, float max)
    {
        var scale = _healthFill.localScale;

        float value = current / max;
        scale = new Vector3(value, scale.y, scale.z);

        if (value <= 0f)
            _healthBar.gameObject.SetActive(false);
    }
}
using UnityEngine;

public class EntityHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _progressBar;

    public void UpdateAmount(float max, float currentAmount)
    {
        float progress = currentAmount / max;
        _progressBar.transform.localScale = new Vector3(progress, 0.8f, 1f);
    }
}
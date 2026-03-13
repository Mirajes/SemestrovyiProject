using UnityEngine;

public class EntityProgressBar : MonoBehaviour
{
    private static GameObject _progressBar;

    public static void UpdateAmount(float max, float currentAmount)
    {
        float progress = currentAmount / max;
        _progressBar.transform.localScale = new Vector3(progress, 0.8f, 1f);
    }

    public void Init()
    {
        if (_progressBar == null)
            _progressBar = transform.GetChild(0).gameObject;
    }
}
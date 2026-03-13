using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public void MoveToHome()
    {
        GameManager.Instance.Player.transform.position = LINKS.Instance.HM_PlayerHomePos.position;
        GameManager.Instance.CameraController.MainCamera.transform.position = GameManager.Instance.CameraController.CameraHomePos;
    }

    public void CraftItem()
    {
        // todo
    }
}
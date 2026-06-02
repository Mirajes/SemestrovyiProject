using UnityEngine;

public class PlayerEntity : A_Entity
{
    [SerializeField] private Rigidbody2D _rb2D;

    public void MoveTo(Transform newTransform)
    {
        this.transform.position = newTransform.position;
        this.transform.rotation = newTransform.rotation;
    }
}

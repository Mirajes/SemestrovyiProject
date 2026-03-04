using UnityEngine;
using UnityEngine.EventSystems;

public class SlotLogic : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("ai");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("trogaut");
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        print("perestali");
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class AreaDragController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] InputData inputData;

    public Vector2 currentPointerPos;
    public Vector2 eventDeltaDrag;

    public void OnDrag(PointerEventData eventData)
    {
        eventDeltaDrag = eventData.delta; // This is per-frame movement
    }

    private void Update()
    {
        currentPointerPos = eventDeltaDrag;

        inputData.xHead = currentPointerPos.x;
        inputData.yHead = currentPointerPos.y;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        eventDeltaDrag = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private RectTransform thumbStickTransform;
    [SerializeField]
    private RectTransform backgroundTransform;
    [SerializeField]
    private RectTransform centerTransform;

    public delegate void OnStickInputValueUpdated(Vector2 inputValue);
    public event OnStickInputValueUpdated onStickValueUpdated;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition = eventData.position;
        Vector2 centerPosition = backgroundTransform.position;
        Vector2 localOffset =
            Vector2.ClampMagnitude(touchPosition - centerPosition, backgroundTransform.sizeDelta.x / 2);
        thumbStickTransform.position = centerPosition + localOffset;
        Vector2 inputValue = localOffset / backgroundTransform.sizeDelta.x / 2;
        onStickValueUpdated?.Invoke(inputValue);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundTransform.position = eventData.position;
        thumbStickTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundTransform.position = centerTransform.position;
        thumbStickTransform.position = backgroundTransform.position;
        onStickValueUpdated?.Invoke(Vector2.zero);
    }
}

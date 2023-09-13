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
    [SerializeField]
    private GameObject[] stickDirections;

    public delegate void OnStickInputValueUpdated(Vector2 inputValue);
    public delegate void OnStickTaped();
    public event OnStickInputValueUpdated onStickValueUpdated;
    public event OnStickTaped onStickTaped;
    private bool wasDragging = false;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition = eventData.position;
        Vector2 centerPosition = backgroundTransform.position;
        Vector2 localOffset =
            Vector2.ClampMagnitude(touchPosition - centerPosition, backgroundTransform.sizeDelta.x / 2);
        thumbStickTransform.position = centerPosition + localOffset;
        Vector2 inputValue = localOffset / (backgroundTransform.sizeDelta.x / 2);
        onStickValueUpdated?.Invoke(inputValue);
        UpdateStickDirectionVisual(inputValue);
        wasDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundTransform.position = eventData.position;
        thumbStickTransform.position = eventData.position;
        wasDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundTransform.position = centerTransform.position;
        thumbStickTransform.position = backgroundTransform.position;
        onStickValueUpdated?.Invoke(Vector2.zero);
        DeactivateAllStickDirections();
        if (!wasDragging)
        {
            onStickTaped?.Invoke();
        }
    }

    private void UpdateStickDirectionVisual(Vector2 inputValue)
    {
        if (   inputValue.x <= 0 
               && inputValue.x >= -1 
               && inputValue.y >= 0 
               && inputValue.y <= 1)
        {
            stickDirections[0].SetActive(true);
            stickDirections[1].SetActive(false);
            stickDirections[2].SetActive(false);
            stickDirections[3].SetActive(false);
        }

        if (   inputValue.x >= 0
               && inputValue.x <= 1
               && inputValue.y >= 0
               && inputValue.y <= 1)
        {
            stickDirections[1].SetActive(true);
            stickDirections[0].SetActive(false);
            stickDirections[2].SetActive(false);
            stickDirections[3].SetActive(false);
        }
        
        if (   inputValue.x >= -1 
               && inputValue.x <= 0 
               && inputValue.y >= -1 
               && inputValue.y <= 0)
        {
            stickDirections[2].SetActive(true);
            stickDirections[0].SetActive(false);
            stickDirections[1].SetActive(false);
            stickDirections[3].SetActive(false);
        }
        
        if (   inputValue.x >= 0 
               && inputValue.x <= 1 
               && inputValue.y >= -1 
               && inputValue.y <= 0)
        {
            stickDirections[3].SetActive(true);
            stickDirections[0].SetActive(false);
            stickDirections[1].SetActive(false);
            stickDirections[2].SetActive(false);
        }
    }

    private void DeactivateAllStickDirections()
    {
        stickDirections[0].SetActive(false);
        stickDirections[1].SetActive(false);
        stickDirections[2].SetActive(false);
        stickDirections[3].SetActive(false);
    }
}

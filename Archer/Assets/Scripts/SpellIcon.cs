using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellIcon : MonoBehaviour , IPointerUpHandler , IDragHandler , IBeginDragHandler , IPointerDownHandler
{
    public Spell spell;

    public void OnDrag(PointerEventData eventData)
    {
        spell.dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        spell.dragging = false;
        spell.PartGo();
        spell.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //...
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        spell.gameObject.SetActive(true);
    }
}

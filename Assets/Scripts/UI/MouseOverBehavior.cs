using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerExit(PointerEventData eventData) {
        Utils.mouseOverUI = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Utils.mouseOverUI = true;
    }
}

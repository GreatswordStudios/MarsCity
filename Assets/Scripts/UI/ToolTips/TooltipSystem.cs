using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public ToolTip tooltip;

    public void Awake(){
        current = this;
    }
    public static void Show(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        Show();
    }

    public static void Show(){
        current.tooltip.gameObject.SetActive(true);
        current.tooltip.MoveToMouse();
    }

    public static void Hide(){
        current.tooltip.gameObject.SetActive(false);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public RectTransform rectTransform;

    public void Awake()
    {
        rectTransform  = GetComponent<RectTransform>();
    }

    public int characterWrapLimit;
    public void SetText(string content, string header = "") 
    {
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else{
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        contentField.text = content;
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        
    }

    public void MoveToMouse() {
        Vector2 position = Input.mousePosition;

        transform.position = position;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    private void Update() {
        MoveToMouse();
    }

    


}

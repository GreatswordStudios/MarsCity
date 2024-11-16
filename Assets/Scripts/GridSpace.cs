using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GridSpace : MonoBehaviour
{   
    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        var mouseRay = Camera.main.ScreenPointToRay(mousePos);
        if (collider.bounds.IntersectRay(mouseRay)){
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    double movementMult = 3;
    double fastMovementMult = 6;
    double zoomMult = 8.0;

    float minZoom = 1;
    float maxZoom = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        float forwardMovement = 0;
        float sideMovement = 0;
        double curMoveMult;
        
        if (Input.GetKey(KeyCode.W)) {
            forwardMovement += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            forwardMovement += -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            sideMovement += 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            sideMovement += -1;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            curMoveMult = fastMovementMult;
        }
        else {
            curMoveMult = movementMult;
        }

        transform.Translate(0, forwardMovement * (float) curMoveMult * Time.deltaTime, 0, Space.Self);
        transform.Translate(sideMovement * (float) curMoveMult * Time.deltaTime, 0, 0, Space.Self);

    	GetComponent<Camera>().orthographicSize += -Input.GetAxis("Mouse ScrollWheel") * (float) zoomMult;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, minZoom, maxZoom);
        
    }
}

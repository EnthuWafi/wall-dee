using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 offset = new Vector3(0f, 5f, -6.5f);

    public Transform lookAt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {     
        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0f;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);

    }
}

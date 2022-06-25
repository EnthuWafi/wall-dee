using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float zBorder = -10;
    public float zPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.z < zBorder)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zPoint);
        }
    }
}

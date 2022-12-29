using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float zBorder = -10;
    public float zBorder_forward = 70;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Destroy game Object
        if (transform.position.z < zBorder)
        {
            Destroy(gameObject);
        }
        if (transform.position.z > zBorder_forward)
        {
            Destroy(gameObject);
        }
    }
}

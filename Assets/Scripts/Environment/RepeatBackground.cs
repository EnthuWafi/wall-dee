using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private BoxCollider myCollider;
    private float startPos;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        startPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < (startPos - (myCollider.size.z / 2)))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos);
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30;
    private GameManager gameManager;
    public AudioClip onHitSFX;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate((gameManager.speed + speed) * Time.deltaTime * Vector3.forward);
        }

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.PlaySound(onHitSFX);
        }
    }
}

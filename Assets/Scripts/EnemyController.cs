using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 10f;

    private PlayerController player;
    private GameManager gameManager;

    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate((gameManager.speed + speed) * Time.deltaTime * Vector3.back);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            ParticleSystem temp = Instantiate(explosionEffect, transform.position, transform.rotation);
            temp.gameObject.AddComponent<MoveBack>();
            temp.gameObject.AddComponent<DestroyOutOfBounds>();

            gameManager.UpdateScore(20);
            gameManager.PlaySound(explosionSound, 2f);
            gameObject.GetComponentInChildren<BoxCollider>().enabled= false;
        }
    }

}

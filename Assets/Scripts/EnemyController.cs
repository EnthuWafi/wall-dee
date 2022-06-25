using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 10f;

    private PlayerController player;
    private GameManager gameManager;

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
            transform.Translate(speed * Time.deltaTime * Vector3.back);
        }
    }

}

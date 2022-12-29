using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;


    //enum
    public enum Lane { LEFT = -1, MIDDLE = 0, RIGHT = 1 };

    //variables
    public float slideSpeed = 5.0f;
    public float jumpHeight = 10.0f;

    public AudioClip jumpSFX;
    public AudioClip shootSFX;
    public AudioClip deathSFX;
    public AudioClip pointSFX;

    public bool isOnGround;
    //private
    private CharacterController controller;
    private GameManager gameManager;

    //public
    public Animator animator;
    public GameObject projectile;

    //jump
    Vector3 jumpVelocity;

    //laning
    public Lane currentLane;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        controller = GetComponent<CharacterController>();

        currentLane = Lane.MIDDLE;
    }

    // Update is called once per frame
    void Update()
    {

        isOnGround = controller.isGrounded;
        if (gameManager.isGameActive)
        {
            PlayerMovement();
            Shoot();
        }
        //jump
        if (isOnGround && Input.GetKeyDown(KeyCode.W) && gameManager.isGameActive)
        {
            jumpVelocity.y = jumpHeight;

            //jump anim
            StartCoroutine(Jump());
        }
        else
        {
            jumpVelocity.y += gameManager.gravityValue * Time.deltaTime;
        }
        controller.Move(jumpVelocity * Time.deltaTime);

    }

    private void PlayerMovement()
    {
        //move lanes
        if (Input.GetKeyDown(KeyCode.A))
        {
            LaneMove(false);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            LaneMove(true);
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (currentLane == Lane.LEFT)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (currentLane == Lane.RIGHT)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * slideSpeed;
        moveVector.y = -4.0f; //gravity

        controller.Move(moveVector * Time.deltaTime);

    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.Point == gameManager.Max_Point)
        {
            Instantiate(projectile, transform.position, projectile.transform.rotation);
            gameManager.PlaySound(shootSFX);

            gameManager.UpdatePoint(Convert.ToInt32(-gameManager.Max_Point));
            
        }
    }
    void LaneMove(bool goingRight)
    {
        if (goingRight)
        {
            if (currentLane != Lane.RIGHT)
                currentLane += 1;
        }
        else
        {
            if (currentLane != Lane.LEFT)
                currentLane -= 1;
        }
    }

    IEnumerator Jump()
    {
        gameManager.PlaySound(jumpSFX);
        animator.SetBool("Roll_Anim", true);

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Roll_Anim", false);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameManager.isGameActive)
            {
                gameManager.PlaySound(deathSFX, 2f);
                
                gameManager.GameOver();
                animator.SetTrigger("Death");

                Debug.Log("Game Over!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            gameManager.PlaySound(pointSFX, 3f); 
            //increments point
            gameManager.UpdatePoint(1);
            gameManager.UpdateScore(10);

            Destroy(other.gameObject);

        }
    }
}

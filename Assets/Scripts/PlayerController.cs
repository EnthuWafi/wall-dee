using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;


    //enum
    public enum Lane {LEFT = -1, MIDDLE = 0 ,RIGHT = 1};


    //variables
    public float slideSpeed  = 5.0f;
    public float jumpHeight = 10.0f;



    public bool isOnGround;

    private float horizontalInput;
    private float verticalInput;
    //private
    private Rigidbody myRigidbody;
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
        myRigidbody = GetComponent<Rigidbody>();
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

        animator.SetBool("Roll_Anim", true);

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Roll_Anim", false);   
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();
            animator.SetTrigger("Death");

            Debug.Log("Game Over!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            //increments point
            gameManager.UpdatePoint(1);
            gameManager.UpdateScore(10);

            Destroy(other.gameObject);

        }
    }
}

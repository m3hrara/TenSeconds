using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float jumpForce = 5;
    [SerializeField]
    float forceMagnitude;
    //components
    private PlayerController playerController;
    Rigidbody rigidbody;
    Animator playerAnimator;
    public GameObject followTarget;

    //references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    public bool isPaused = false;

    //animator hashes
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        isPaused = false;
    }

    void Update()
    {
        if(!isPaused && !gameManager.isPaused)
        {
            //movement

            if (playerController.isJumping) return;
            if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

            moveDirection = transform.right * inputVector.x;
            float currentSpeed = walkSpeed;

            Vector3 movementDirection = moveDirection * (-currentSpeed * Time.deltaTime);
            transform.position += movementDirection;
        }

    }

    public void OnMovement(InputValue value)
    {
        if (!isPaused && !gameManager.isPaused)
        {
            inputVector = value.Get<Vector2>();
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);
        }
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);
    }

    public void OnJump(InputValue value)
    {
        if(playerController.isJumping)
        {
            return;
        }
        playerController.isJumping = value.isPressed;
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
        rigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Laser"))
        //{
        //    Time.timeScale = 0;
        //    gameManager.isPaused = true;
        //    gameManager.winLose.text = "YOU LOST!";
        //    gameManager.endPanel.SetActive(true);
        //}
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            StartCoroutine(slowDown());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, false);
        if(collision.gameObject.CompareTag("moveable"))
        {
            Rigidbody rb = collision.collider.attachedRigidbody;
            if(rb !=null)
            {
                Vector3 forceDir = collision.gameObject.transform.position - transform.position;
                forceDir.y = 0;
                forceDir.Normalize();
                rb.AddForceAtPosition(forceDir * forceMagnitude, transform.position, ForceMode.Impulse);
            }
        }
        if (collision.gameObject.CompareTag("Cat"))
        {
            gameManager.audioSource.Play();
            gameManager.timeLeft += 2f;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            walkSpeed = 2f;
        }

    }
    
    IEnumerator slowDown()
    {
        for(float speed =2f; speed<= 5f; speed+=0.1f)
        {
            walkSpeed = speed;
            yield return null;
        }
    }
}
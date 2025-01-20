using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float moveSpeed = 5f;          
    public float rotationSpeed = 700f;    
    public float gravity = -9.8f;         
    public float jumpHeight = 2f;         

    private CharacterController characterController;
    private Vector3 velocity;
    private float currentSpeed;

    public Transform playerCamera;        
    public float rotationSmoothTime = 0.1f; 
    private float rotationVelocity;

    private float speedBoostMultiplier = 1f; 
    private float speedBoostTimer = 0f; 

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();

        
        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                DeactivateSpeedBoost();
            }
        }
    }

    private void HandleMovement()
    {
        // Get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        Vector3 direction = (playerCamera.forward * vertical) + (playerCamera.right * horizontal);
        direction.Normalize(); 

        
        currentSpeed = moveSpeed * speedBoostMultiplier;
        characterController.Move(direction * currentSpeed * Time.deltaTime);

        
        if (characterController.isGrounded)
        {
            velocity.y = -2f; 
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        
        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        if (horizontal != 0 || vertical != 0)
        {
            float targetAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    
    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        speedBoostMultiplier = multiplier;
        speedBoostTimer = duration;
    }

    
    private void DeactivateSpeedBoost()
    {
        speedBoostMultiplier = 1f; 
        speedBoostTimer = 0f;
    }
}

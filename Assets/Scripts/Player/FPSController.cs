using UnityEditor;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraHolder;

    [Header("Base Movement")]
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float playerMass = 50.0f;

    [Header("Advanced Movement")]
    public float maxSpeed = 6f;
    public float acceleration = 15f;
    public float friction = 8f;
    public float airAcceleration = 2f;

    [Header("Look")]
    public float mouseSensitivity = 2.0f;

    private CharacterController controller;
    private float xRotation;

    private Vector3 velocity;
    //private Vector3 electricVelocity;

    public ElectricField activeField;

    PlayerCharge playerChargeComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerChargeComponent = GetComponentInParent<PlayerCharge>();

        // lock the mouse to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleLook ()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;


        // left/right rotates player body
        transform.Rotate(Vector3.up * mouseX);

        //up/down only rotates camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleMovement ()
    {
        bool grounded = controller.isGrounded;

        // reset falling speed when on ground
        if (grounded)
        {
            ApplyFriction();
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // the direction the player wants to move
        Vector3 wantDir = transform.right * x + transform.forward * z;
        wantDir.Normalize();

        float currAcceleration = grounded ? acceleration : airAcceleration;
        Accelerate(wantDir, maxSpeed, currAcceleration);
        

        // If the player is experiencing an electric force, find the vector
        if (activeField != null)
        {
            Vector3 E = activeField.GetElectricField(transform.position);
            float q = 0;

            switch(playerChargeComponent.playerCharge)
            {
                case ChargeType.Positive:
                    q = 1;
                    break;
                
                case ChargeType.Negative:
                    q = -1;
                    break;

                default:
                    break;
            }

            Vector3 acceleration = PhysicsEquations.CalculateAcceleration(q * E, playerMass);
            Debug.Log("acc: " + acceleration);
            velocity += acceleration * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move (velocity  * Time.deltaTime);
    }

    void ApplyFriction ()
    {
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);

        float speed = horizontalVelocity.magnitude;

        if (speed < 0.01f)
        {
            return;
        }

        float drop = speed * friction * Time.deltaTime;

        float newSpeed = Mathf.Max(speed - drop, 0);

        float ratio = newSpeed / speed;

        velocity.x *= ratio;
        velocity.z *= ratio;
    }
    
    void Accelerate (Vector3 direction, float targetSpeed, float accel)
    {
        float currSpeed = Vector3.Dot(velocity, direction);
        float addSpeed = targetSpeed - currSpeed;

        if (addSpeed <= 0)
        {
            return;
        }

        float accelAmount = accel * Time.deltaTime * targetSpeed;

        accelAmount = Mathf.Min(accelAmount, addSpeed);
        velocity += direction * accelAmount;
        
    }
}

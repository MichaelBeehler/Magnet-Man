using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraHolder;

    [Header("Movement")]
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float playerMass = 50.0f;

    [Header("Look")]
    public float mouseSensitivity = 2.0f;

    private CharacterController controller;
    private float xRotation;

    private Vector3 velocity;
    private Vector3 electricVelocity;

    public ChargedObject activeField;

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
        Debug.Log(activeField);
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
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // move direction relative to direction player is facing
        Vector3 move = transform.right * x + transform.forward * z;

        // If the player is experiencing an electric force, find the vector
        if (activeField != null)
        {
            Vector3 forceVector = CalculateElectricForceVector();

            Vector3 acceleration = PhysicsEquations.CalculateAcceleration(forceVector, playerMass);

            // If object and player are not neutral, when we will be able to attract or repel
            if (activeField.charge != ChargeType.Neutral && playerChargeComponent.playerCharge != ChargeType.Neutral)
            {
                if (activeField.charge != playerChargeComponent.playerCharge)
                {
                    electricVelocity += acceleration * Time.deltaTime;
                    Debug.Log(electricVelocity);
                }
                else
                {
                    //move-= forceVector;
                    electricVelocity -= acceleration * Time.deltaTime;
                    Debug.Log(electricVelocity);
                }
            }
        }
        
        controller.Move (move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move ((velocity + electricVelocity )  * Time.deltaTime);
        
    }

    // Returns a Vector3 describing the net electrical force experienced by the player
    Vector3 CalculateElectricForceVector ()
    {
        Vector3 objPos = activeField.transform.position;
        Vector3 playerPos = transform.position;
        Vector3 heading = objPos - playerPos;
        Vector3 normalizedHeading = heading.normalized;

        float squareMagnitude = heading.sqrMagnitude;

        float force = PhysicsEquations.CalculatePointChargeForceSqDist(100,100, squareMagnitude);

        return normalizedHeading * force;
    }

}

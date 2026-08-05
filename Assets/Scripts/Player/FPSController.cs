using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FPSController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraHolder;

    [Header("Base Movement")]
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

    [Header("Sprinting")]
    public float sprintMultiplier = 1.5f;
    public KeyCode sprintKey = KeyCode.LeftShift;

    private CharacterController controller;
    private float xRotation;

    private Vector3 velocity;

    public List<ElectricField> activeFields = new List<ElectricField>();
    public List<MagneticField> activeMagneticFields = new List<MagneticField>();

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
            if (velocity.y < -2f)
            {
                velocity.y = -2f;
            }
        }
        ApplyMovementInput(grounded);
        ApplyElectricFields();
        ApplyMagneticFields();
        ApplyJump(grounded);
        ApplyGravity();
        MoveCharacter();

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

    void ApplyElectricFields()
    {
        Vector3 netField = Vector3.zero;

        foreach (ElectricField field in activeFields)
        {
            netField += field.GetElectricField(transform.position);
        }

        float q = 0;

        switch (playerChargeComponent.playerCharge)
        {
            case ChargeType.Positive:
                q = 1;
                break;
            
            case ChargeType.Negative:
                q = -1;
                break;

            // No need to continue, not affected by fields
            case ChargeType.Neutral:
                return;
        }

        Vector3 acceleration = PhysicsEquations.CalculateAcceleration(q * netField, playerMass);

        velocity += acceleration * Time.deltaTime;
    }

    void ApplyMagneticFields ()
    {
        Vector3 netfield = Vector3.zero;

        foreach (MagneticField field in activeMagneticFields)
        {
            netfield += field.GetMagneticField(transform.position);
        }

        float q = 0;

        switch (playerChargeComponent.playerCharge)
        {
            case ChargeType.Positive:
                q = 1;
                break;

            case ChargeType.Negative:
                q = -1;
                break;

            default:
            return;
        }

        Vector3 horizVelocity = new Vector3(velocity.x, 0, velocity.z);

        // F = q( v x B )
        Vector3 magneticForce = q * Vector3.Cross(horizVelocity, netfield);

        Vector3 magneticAcceleration = PhysicsEquations.CalculateAcceleration(magneticForce, playerMass);

        velocity += magneticAcceleration * Time.deltaTime;
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    void ApplyJump(bool grounded)
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyMovementInput (bool grounded)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 wishDir = transform.right * x + transform.forward * z;

        wishDir.Normalize();

        bool sprinting = Input.GetKey(sprintKey) && z > 0.1f;

        float targetSpeed = sprinting ? maxSpeed * sprintMultiplier : maxSpeed;

        float accel = grounded ? acceleration : airAcceleration;

        Accelerate(wishDir, targetSpeed, accel);
    }

    void MoveCharacter ()
    {
        CollisionFlags flags = controller.Move(velocity * Time.deltaTime);

        Debug.Log(flags);

        if ((flags & CollisionFlags.Above) != 0)
        {
            Debug.Log("Hit ceiling!");
            velocity.y = -2.0f;
            //velocity = Vector3.zero;
        }
    }
    
    void OnGUI()
    {
        GUI.skin.label.fontSize = 25;
        GUI.Label(
            new Rect(10,10,300,100),
            "Velocity: " + velocity.magnitude.ToString("F2")
        );
    }
}

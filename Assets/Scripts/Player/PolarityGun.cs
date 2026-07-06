using UnityEngine;

public class PolarityGun : MonoBehaviour
{   
    ChargedObject selectedChargedObject;
    Rigidbody selectedRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // If left mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            ProcessMouse();
        }

        // Right Mouse Click
        else if (Input.GetMouseButtonDown(1))
        {
            // De-Select Charged Object
            selectedChargedObject = null;
            selectedRb = null;
        }

        if (selectedChargedObject != null)
        {
            ApplyElectricForce(selectedChargedObject);
        }     
    }

    void ProcessMouse ()
    {
        // Create a RaycastHit object, which will contain info on clicked objects
        RaycastHit hit;

        // If something was hit, print its name in the console
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Does the hit object have a ChargedObject Component?
            selectedChargedObject = hit.collider.GetComponent<ChargedObject>();
            if (selectedChargedObject)
            {
                Debug.Log("This object is charged");
                selectedRb = selectedChargedObject.GetComponent<Rigidbody>();
            }
        }
    }

    // If the player left clicks a charged object, select it, pulling towards camera
    void SelectObject ()
    {
        
    }

    void ApplyElectricForce (ChargedObject selectedChargedObject)
    {
        // Compute direction from selected object towards player
        ComputeDirection(selectedChargedObject, out Vector3 heading, out float squareMagnitude);

        // If within certain range, don't apply force
        if (squareMagnitude < 4)
        {
            return;
        }
        // Normalize the vector
        Vector3 dir = heading.normalized;
        
        //Debug.Log("Velocity: " + selectedChargedObject.GetComponent<Rigidbody>().linearVelocity.magnitude);

        // Add force to the hit object
        /*Rigidbody rigidbody = selectedChargedObject.rb;
        float electricForceMagnitude = ElectricForceCalculator.CalculatePointChargeForceSqDist(1, 1, squareMagnitude);
        rigidbody.AddForce(5 * dir * electricForceMagnitude);
        //selectedObject.attachedRigidbody.AddForce(dir * 500);*/
        ApplyForce(dir, squareMagnitude, selectedChargedObject);
    }

    void ComputeDirection (ChargedObject obj, out Vector3 heading, out float squareMagnitude)
    {
        Vector3 start = obj.transform.position;
        Vector3 target = transform.position;

        heading = target - start;
        squareMagnitude = heading.sqrMagnitude;
    }

    void ApplyForce (Vector3 normalizedDirection, float sqrmag, ChargedObject selectedChargedObject)
    {
        // Get chargedObject's rigidbody
        Rigidbody rigidbody = selectedChargedObject.rb;

        float electricForceMagnitude = PhysicsEquations.CalculatePointChargeForceSqDist(10, 10, sqrmag);

        Debug.Log(selectedChargedObject);
        Debug.Log(selectedChargedObject.rb);

        PlayerCharge pc = GetComponentInParent<PlayerCharge>();
        Debug.Log(pc);

        // If object and player are not neutral, when we will be able to attract or repel
        if (selectedChargedObject.charge != ChargeType.Neutral && GetComponentInParent<PlayerCharge>().playerCharge != ChargeType.Neutral)
        {
            if (selectedChargedObject.charge != GetComponentInParent<PlayerCharge>().playerCharge)
            {
                rigidbody.AddForce(5 * normalizedDirection * electricForceMagnitude);
            }
            else
            {
                rigidbody.AddForce(5 * normalizedDirection * -electricForceMagnitude);
            }
        }
    }
}

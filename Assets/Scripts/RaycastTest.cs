using UnityEngine;

public class RaycastTest : MonoBehaviour
{   
    ChargedObject selectedChargedObject;
    Rigidbody selectedRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If left mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            ProcessMouse(0);
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
            PullChargedObject(selectedChargedObject);
        }     
    }

    void ProcessMouse (int buttonPressed)
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

    void PullChargedObject (ChargedObject selectedChargedObject)
    {
        // Compute direction from cube towards player
        Vector3 start = selectedChargedObject.transform.position;
        Vector3 target = transform.position;
        Vector3 heading = target - start;
        float squareMagnitude = heading.sqrMagnitude;

        if (squareMagnitude < 25)
        {
            return;
        }
        // Normalize the vector
        Vector3 dir = heading.normalized;
        Debug.Log("Dir: " + dir);

        // Add force to the hit object
        Rigidbody rigidbody = selectedChargedObject.rb;
        rigidbody.AddForce(dir * 5);
        //selectedObject.attachedRigidbody.AddForce(dir * 500);
    }
}

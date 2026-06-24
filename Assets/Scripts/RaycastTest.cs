using UnityEngine;

public class RaycastTest : MonoBehaviour
{

    GameObject selectedObject;

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
            selectedObject = null;
        }

        if (selectedObject != null)
        {
            PullChargedObject(selectedObject);
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
            ChargedObject chargedObject = hit.collider.GetComponent<ChargedObject>();
            if (chargedObject)
            {
                Debug.Log("This object is charged");
                selectedObject = hit.collider.gameObject;
            }
        }
    }

    // If the player left clicks a charged object, select it, pulling towards camera
    void SelectObject ()
    {
        
    }

    void PullChargedObject (GameObject selectedObject)
    {
        // Compute direction from cube towards player
        Vector3 start = selectedObject.transform.position;
        Vector3 target = transform.position;
        Vector3 heading = target - start;

        // Normalize the vector
        Vector3 dir = heading.normalized;
        Debug.Log("Dir: " + dir);

        // Add force to the hit object
        ChargedObject charge = selectedObject.GetComponent<ChargedObject>();
        Rigidbody rigidbody = charge.GetComponent<Rigidbody>();
        rigidbody.AddForce(dir * 5);
        //selectedObject.attachedRigidbody.AddForce(dir * 500);
    }
}

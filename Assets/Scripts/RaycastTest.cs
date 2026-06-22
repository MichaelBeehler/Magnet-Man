using UnityEngine;

public class RaycastTest : MonoBehaviour
{
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
            processMouse(0);
        }

        else if (Input.GetMouseButtonDown(1))
        {
            processMouse(1);
        }     
    }

    void processMouse (int buttonPressed)
    {
        // Create a RaycastHit object, which will contain info on clicked objects
        RaycastHit hit;

        // If something was hit, print its name in the console
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Does the hit object have a MagneticObject Component?
            if (hit.collider.GetComponent<MagneticObject>())
            {
                Debug.Log("This object is magnetic");

                // Compute direction from cube towards player
                Vector3 start = hit.collider.transform.position;
                Vector3 target = transform.position;
                Vector3 heading = target - start;

                // Normalize the vector
                Vector3 dir = heading.normalized;
                Debug.Log("Dir: " + dir);

                // Add force to the hit object
                if (buttonPressed == 0)
                {
                    hit.collider.attachedRigidbody.AddForce(dir * 500);
                }

                else 
                {
                    hit.collider.attachedRigidbody.AddForce(-dir * 500);
                }
            }
        }
    }
}

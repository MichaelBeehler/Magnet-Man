using UnityEngine;

public class ElectricFieldTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Entered Zone");
        }
    }
    private void OnTriggerStay (Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("In Zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Left Zone");
        }
    }
}
using UnityEngine;

public class ElectricFieldTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            FPSController playerController = other.transform.root.GetComponent<FPSController>();
            ElectricField field = GetComponentInParent<ElectricField>();

            if (!playerController.activeFields.Contains(field))
            {
                playerController.activeFields.Add(field);
            }
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
            FPSController playerController = other.transform.root.GetComponent<FPSController>();
            ElectricField field = GetComponentInParent<ElectricField>();
            playerController.activeFields.Remove(field);
        }
    }
}
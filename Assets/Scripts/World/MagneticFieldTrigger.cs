using Unity;
using UnityEngine;

public class MagneticFieldTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            FPSController playerController = other.transform.root.GetComponent<FPSController>();
            MagneticField field = GetComponentInParent<MagneticField>();

            if (!playerController.activeMagneticFields.Contains(field))
            {
                playerController.activeMagneticFields.Add(field);
                Debug.Log("Added Magnetic Field");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            FPSController playerController = other.transform.root.GetComponent<FPSController>();

            MagneticField field = GetComponentInParent<MagneticField>();

            playerController.activeMagneticFields.Remove(field);

            Debug.Log ("Removed Magnetic Field");
        }
    }
}
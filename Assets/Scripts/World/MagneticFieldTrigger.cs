using System.Drawing;
using Unity;
using UnityEngine;

public class MagneticFieldTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MagneticField field = GetComponentInParent<MagneticField>();

        // Player
        if (other.transform.root.CompareTag("Player"))
        {
            FPSController playerController = other.transform.root.GetComponent<FPSController>();

            if (!playerController.activeMagneticFields.Contains(field))
            {
                playerController.activeMagneticFields.Add(field);
                //Debug.Log("Added Magnetic Field");
            }
        }

        PointCharge pointCharge = other.transform.root.GetComponent<PointCharge>();

        if (pointCharge != null)
        {
            if (!pointCharge.activeMagneticFields.Contains(field))
            {
                pointCharge.activeMagneticFields.Add(field);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MagneticField field = GetComponentInParent<MagneticField>();


        // Player
        if (other.transform.root.CompareTag("Player"))
        {
            FPSController playerController = other.transform.root.GetComponent<FPSController>();

            playerController.activeMagneticFields.Remove(field);

            Debug.Log ("Removed Magnetic Field");
        }

        // Point Charge
        PointCharge pointCharge = other.transform.root.GetComponent<PointCharge>();
        
        if (pointCharge != null)
        {
            pointCharge.activeMagneticFields.Remove(field);
        }
    }
}
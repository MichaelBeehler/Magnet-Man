using UnityEngine;

public class ElectricFieldTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        ElectricField field = GetComponentInParent<ElectricField>();
        // Player
        if (other.transform.root.CompareTag("Player"))
        {
            FPSController playerController = other.transform.root.GetComponent<FPSController>();

            if (!playerController.activeFields.Contains(field))
            {
                playerController.activeFields.Add(field);
            }
        }

        // Point Charge (our spheres)
        PointCharge pointCharge = other.transform.root.GetComponent<PointCharge>();

        if (pointCharge != null)
        {
            if (!pointCharge.activeElectricFields.Contains(field))
            {
                pointCharge.activeElectricFields.Add(field);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ElectricField field = GetComponentInParent<ElectricField>();

        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Left Zone");
            FPSController playerController = other.transform.root.GetComponent<FPSController>();
            playerController.activeFields.Remove(field);
        }

        PointCharge pointCharge = other.transform.root.GetComponent<PointCharge>();

        if (pointCharge != null)
        {
            pointCharge.activeElectricFields.Remove(field);
        }
    }
}
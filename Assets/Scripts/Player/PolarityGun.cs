using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PolarityGun : MonoBehaviour
{   
    private PointCharge selectedPointCharge;

    private PlayerCharge playerCharge;

    void Start()
    {
        playerCharge = GetComponentInParent<PlayerCharge>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeselectObject();
        }

        if (selectedPointCharge != null)
        {
            ApplyElectricInteraction();
        }
    }

    void SelectObject()
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            return;
        }

        PointCharge pointCharge = hit.collider.GetComponentInParent<PointCharge>();
        if (pointCharge == null)
        {
            return;
        }

        selectedPointCharge = pointCharge;
        Debug.Log("Selected: " + pointCharge.name);
    }

    void DeselectObject()
    {
        selectedPointCharge = null;
    }

    void ApplyElectricInteraction()
    {
        selectedPointCharge.ApplyForceFromPointCharge(transform.root.position, playerCharge.playerCharge, 10f);
    }
}

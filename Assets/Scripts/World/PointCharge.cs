using UnityEngine;
using System.Collections.Generic;

public class PointCharge : MonoBehaviour
{
    public ChargeType charge = ChargeType.Neutral;

    public Rigidbody rb;

    public List<ElectricField> activeElectricFields = new List<ElectricField>();

    public List<MagneticField> activeMagneticFields = new List<MagneticField>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyElectricFields();
        ApplyMagneticFields();
    }

    void ApplyElectricFields()
    {
        if (charge == ChargeType.Neutral)
        {
            return;
        }

        // The value of the charge. Currently, we just use 1 and -1, but can expand on this to include more or variable charge values later
        float q = charge == ChargeType.Positive ? 1f : -1f;

        Vector3 netField = Vector3.zero;

        foreach (ElectricField field in activeElectricFields)
        {
            netField += field.GetElectricField(transform.position);
        }

        Vector3 force = q * netField;

        rb.AddForce(force);
    }

    void ApplyMagneticFields()
    {
        if (charge == ChargeType.Neutral)
        {
            return;
        }

        // If charge is positive, set q to 1, if negative make it -1
        float q = charge == ChargeType.Positive ? 1f : -1f;

        Vector3 netField = Vector3.zero;

        foreach (MagneticField field in activeMagneticFields)
        {
            netField += field.GetMagneticField(transform.position);
        }

        Vector3 magneticForce = q * Vector3.Cross(rb.linearVelocity, netField);

        rb.AddForce(magneticForce);
    }

    public void ApplyForceFromPointCharge(Vector3 sourcePosition, ChargeType sourceCharge, float sourceChargeMagnitude)
    {
        if (charge == ChargeType.Neutral || sourceCharge == ChargeType.Neutral)
        {
            return;
        }

        Vector3 direction = transform.position - sourcePosition;
        float distSquared = direction.sqrMagnitude;

        // Don't allow extreme forces (which occurs when player gets too close)
        if (distSquared < 0.01f)
        {
            return;
        }

        direction.Normalize();

        float forceMagnitude = PhysicsEquations.CalculatePointChargeForceSqDist(sourceChargeMagnitude, 1f, distSquared);

        // Like charges repel each other
        if (charge == sourceCharge)
        {
            rb.AddForce(direction * forceMagnitude);
        }

        // Opposites attract
        else
        {
            rb.AddForce(-direction * forceMagnitude);
        }
    }
}
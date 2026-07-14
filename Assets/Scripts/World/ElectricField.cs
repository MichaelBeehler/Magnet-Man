using UnityEngine;

public abstract class ElectricField : MonoBehaviour
{
    [Header("Electric Field")]
    public ChargeType charge;
    public float fieldStrength;

    public abstract Vector3 GetElectricField (Vector3 position);
}
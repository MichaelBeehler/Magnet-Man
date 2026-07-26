using UnityEngine;

public abstract class ElectricField : MonoBehaviour
{
    [Header("Electric Field")]
    public ChargeType charge;
    public float fieldStrength;

    public abstract Vector3 GetElectricField (Vector3 position);
    public void UpdateColor ()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (charge == ChargeType.Neutral)
        {
            renderer.material.color = Color.gold;
        }

        else if (charge == ChargeType.Positive)
        {
            renderer.material.color = Color.red;
        }

        else
        {
            renderer.material.color = Color.blue;
        }
    }
}
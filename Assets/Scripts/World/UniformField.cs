using UnityEngine;

public class UniformField : ElectricField
{
    public Vector3 fieldDirection;
    

    public void Start()
    {
        fieldDirection = transform.up;
    } 

    public override Vector3 GetElectricField(Vector3 position)
    {

        if (charge == ChargeType.Positive)
        {
            return fieldDirection * fieldStrength;
        }

        else if (charge == ChargeType.Negative)
        {
            return  - fieldDirection * fieldStrength;
        }

        else
        {
            return Vector3.zero;
        }
    }
}
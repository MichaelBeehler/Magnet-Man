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
        return fieldDirection * fieldStrength;
    }

}
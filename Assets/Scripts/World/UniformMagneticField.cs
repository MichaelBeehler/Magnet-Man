using UnityEngine;

public class UniformMagneticField : MagneticField
{
    private Vector3 fieldDirection;
    public bool applyAsImpulse;

    void Start()
    {
        fieldDirection = transform.up;
    }

    public override Vector3 GetMagneticField(Vector3 position)
    {
        return fieldDirection * fieldStrength;
    }
}
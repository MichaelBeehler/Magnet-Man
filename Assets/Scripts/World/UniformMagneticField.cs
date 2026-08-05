using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class UniformMagneticField : MagneticField
{
    private Vector3 fieldDirection;

    void Start()
    {
        fieldDirection = transform.up;
    }

    public override Vector3 GetMagneticField(Vector3 position)
    {
        return fieldDirection * fieldStrength;
    }
}
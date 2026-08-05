using UnityEngine;

public abstract class MagneticField : MonoBehaviour
{
    public float fieldStrength;

    public abstract Vector3 GetMagneticField(Vector3 position);
}
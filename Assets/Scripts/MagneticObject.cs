using UnityEngine;

public class MagneticObject : MonoBehaviour
{
   public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

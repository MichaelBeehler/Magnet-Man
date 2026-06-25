using UnityEngine;

public class ChargedObject : MonoBehaviour
{
   public Rigidbody rb;
   public ChargeType charge;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

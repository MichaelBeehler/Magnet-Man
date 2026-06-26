using UnityEngine;

public class ChargedObject : MonoBehaviour
{
   public Rigidbody rb;

   // Does this object have a positive or negative (or neutral) charge?
   public ChargeType charge;

   // How strong is the charge of this object?
   public int chargeStrength;

   // Can this object be affected/moved by forces?
   public bool canMove;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

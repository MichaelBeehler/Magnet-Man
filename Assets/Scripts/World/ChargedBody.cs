using UnityEngine;

public class ChargedBody : MonoBehaviour
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
        UpdateColor();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateColor ()
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

using UnityEngine;

public class PlayerCharge : MonoBehaviour
{

    public ChargeType playerCharge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            playerCharge = ChargeType.Positive;
            Debug.Log("Player is now positive");
        }

        else if (Input.GetKeyDown("2"))
        {
            playerCharge = ChargeType.Negative;
            Debug.Log("Player is now negative");
        }

        else if (Input.GetKeyDown("3"))
        {
            playerCharge = ChargeType.Neutral;
            Debug.Log("Player is now neutral");
        }
    }

    // Change a player's charge, and play a sound (eventually)
    void SetCharge ()
    {
        
    }
}

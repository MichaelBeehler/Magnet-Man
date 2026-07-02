using UnityEngine;

public class PlayerCharge : MonoBehaviour
{

    public ChargeType playerCharge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Players should start as neutral, to avoid weird/unintended pushing and pulling
        playerCharge = ChargeType.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            playerCharge = ChargeType.Positive;
            SetPlayerColor();
            Debug.Log("Player is now positive");
        }

        else if (Input.GetKeyDown("2"))
        {
            playerCharge = ChargeType.Negative;
            SetPlayerColor();
            Debug.Log("Player is now negative");
        }

        else if (Input.GetKeyDown("3"))
        {
            playerCharge = ChargeType.Neutral;
            SetPlayerColor();
            Debug.Log("Player is now neutral");
        }
    }

    // Change a player's charge, and play a sound (eventually)
    void SetCharge ()
    {
        
    }

    void SetPlayerColor ()
    {
        Renderer playerRenderer = GetComponentInChildren<Renderer>();

        if (playerCharge == ChargeType.Positive)
        {
            playerRenderer.material.color = Color.red;
        }

        else if (playerCharge == ChargeType.Negative)
        {
            playerRenderer.material.color = Color.blue;
        }

        else
        {
            playerRenderer.material.color = Color.gold;
        }
    }
}

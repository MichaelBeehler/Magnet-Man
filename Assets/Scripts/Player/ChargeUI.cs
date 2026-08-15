using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChargeUI : MonoBehaviour
{
    public PlayerCharge playerCharge;
    public Image chargeIndicator;
    public TMP_Text chargeLabel;

    void Update()
    {
        switch (playerCharge.playerCharge)
        {
            case ChargeType.Positive:
                chargeIndicator.color = Color.red;
                chargeLabel.text = "POSITIVE";
                break;
            
            case ChargeType.Negative:
                chargeIndicator.color = Color.blue;
                chargeLabel.text = "NEGATIVE";
                break;
            
            case ChargeType.Neutral:
                chargeIndicator.color = Color.gray;
                chargeLabel.text = "NEUTRAL";
                break;
        }
    }
}
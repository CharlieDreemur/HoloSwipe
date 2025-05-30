using UnityEngine;


[CreateAssetMenu(fileName = "SpeedMerchEffect", menuName = "Scriptable Objects/Merch Effect/PickUpBonus")]
public class PickUpBonusMerchEffect : MerchEffect
{
    public float pickUpBonus;

    public override string GetEffectString()
    {
        return "+" + pickUpBonus + " bonus on pick up money";
    }

    public override float GetStatValue()
    {
        return pickUpBonus;
    }

    public override int GetEffectType()
    {
        return 7;
    }
}


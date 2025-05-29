using UnityEngine;

[CreateAssetMenu(fileName = "DiscountMerchEffect", menuName = "Scriptable Objects/Merch Effect/DiscountMerchEffect")]
public class DiscountMerchEffect : MerchEffect
{
    public int value;

    public override string GetEffectString()
    {
        return "+" + value + "% discount";
    }

    public override float GetStatValue()
    {
        return value;
    }

    public override int GetEffectType()
    {
        return 4;
    }
}

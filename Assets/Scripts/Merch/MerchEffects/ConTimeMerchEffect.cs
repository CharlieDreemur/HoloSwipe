using UnityEngine;

[CreateAssetMenu(fileName = "ConTimeMerchEffect", menuName = "Scriptable Objects/Merch Effect/ConTimeMerchEffect")]
public class ConTimeMerchEffect : MerchEffect
{
    public int conTime;

    public override string GetEffectString()
    {
        return "+" + conTime + " Con Time Tomorow";
    }

    public override float GetStatValue()
    {
        return conTime;
    }

    public override int GetEffectType()
    {
        return 6;
    }
}

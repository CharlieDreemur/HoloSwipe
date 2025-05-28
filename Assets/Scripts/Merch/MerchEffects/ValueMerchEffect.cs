using UnityEngine;

[CreateAssetMenu(fileName = "ValueMerchEffect", menuName = "Scriptable Objects/Merch Effect/ValueMerchEffect")]
public class ValueMerchEffect : MerchEffect
{
    public int value;

    public override void EndScreenEffect(EndOfRoundManager endOfRoundManager)
    {
        endOfRoundManager.merchValue += value;
    }

    public override string GetEffectString()
    {
        return "+" + value + " fan score";
    }

    public override float GetStatValue()
    {
        return value;
    }
    public override int GetEffectType()
    {
        return 1;
    }
}

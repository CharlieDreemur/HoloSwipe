using UnityEngine;

[CreateAssetMenu(fileName = "LuckMerchEffect", menuName = "Scriptable Objects/Merch Effect/LuckMerchEffect")]
public class LuckMerchEffect : MerchEffect
{
    //public int value;

    public override void EndScreenEffect(EndOfRoundManager endOfRoundManager)
    {
        endOfRoundManager.merchValue += value;
    }

    public override string GetEffectString()
    {
        return "+" + value + " luck";
    }

    public override int GetEffectType()
    {
        return 5;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "ValueMerchEffect", menuName = "Scriptable Objects/Merch Effect/ValueMerchEffect")]
public class ValueMerchEffect : MerchEffect
{
    public int value;

    public override void EndScreenEffect(EndOfRoundManager endOfRoundManager)
    {
        endOfRoundManager.merchValue += value;
    }
}

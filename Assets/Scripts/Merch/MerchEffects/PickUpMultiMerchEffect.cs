using UnityEngine;


[CreateAssetMenu(fileName = "SpeedMerchEffect", menuName = "Scriptable Objects/Merch Effect/PickUpMulti")]
public class PickUpMultiMerchEffect : MerchEffect
{
    public float percentMulti;

    public override string GetEffectString()
    {
        return "+" + percentMulti + "bonus on pickup";
    }

    public override float GetStatValue()
    {
        return percentMulti;
    }

    public override int GetEffectType()
    {
        return 8;
    }
}

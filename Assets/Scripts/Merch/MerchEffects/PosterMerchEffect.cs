using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PosterMerchEffect", menuName = "Scriptable Objects/Merch Effect/PosterMerchEffect")]
public class PosterMerchEffect : MerchEffect
{
    public MerchEffect merchEffectChild;
    public MerchCollcetion posterCollection;

    public override string GetEffectString()
    {
        return merchEffectChild.GetEffectString() + " per UNIQuE poster (Current " + GetUniquePosters() + ")";
    }

    public int GetUniquePosters() 
    {
        HashSet<MerchSO> posters = new HashSet<MerchSO>(posterCollection.merches);

        int amount = 0;

        foreach (var item in GameManager.instance.merch)
        {
            if (posters.Contains(item.merch)) 
            {
                posters.Remove(item.merch);
                amount++;
            }
        }

        return amount;
    }

    public override float GetStatValue()
    {
        return merchEffectChild.GetStatValue() * GetUniquePosters();
    }

    public override int GetEffectType()
    {
        return merchEffectChild.GetEffectType();
    }
}

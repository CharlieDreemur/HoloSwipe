using UnityEngine;

[System.Serializable]
public class MerchInstance
{
    public MerchSO merch;
    public int inventoryRotIndex;
    public Vector2Int inventoryPosition;

    public MerchInstance(MerchSO newMerch) 
    {
        merch = newMerch;
    }


    public string GetDescription()
    {
        string description = "";

        foreach (var item in merch.effects)
        {
            description += item.GetEffectString();
            description += "<br>";
        }

        return description;
    }
}

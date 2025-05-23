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
}

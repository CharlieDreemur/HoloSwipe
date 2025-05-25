using UnityEngine;

public class BoothManager : MonoBehaviour
{
    [SerializeField] MerchBehaviour[] merchBehaviours;

    [SerializeField] MerchSO[] merchPool;

    private void Start()
    {
        StockShop();
    }

    public void StockShop() 
    {
        foreach (var item in merchBehaviours)
        {
            item.SetMerch(GetRandomMerch());
        }
    }

    public MerchSO GetRandomMerch() 
    {
        return merchPool[Random.Range(0, merchPool.Length)];
    }
}

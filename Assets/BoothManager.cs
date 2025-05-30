using UnityEngine;

public class BoothManager : MonoBehaviour
{
    [SerializeField] MerchBehaviour[] merchBehaviours;

    MerchSO[] merchPool;
    [SerializeField] MerchCollcetion merchPoolCollction;
    private void Start()
    {

        if(merchPoolCollction != null)
            merchPool = merchPoolCollction.merches;
        
        else if(GameManager.instance != null)
            merchPool = GameManager.instance.merchPool.merches;
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] float pickUpRange;
    [SerializeField] LayerMask merchLayer;
    [SerializeField] int money;

    public List<MerchSO> playersMerch = new List<MerchSO>();

    [SerializeField] MerchDisplayUI merchDisplay;

    MerchBehaviour nearbyMerch;

    public PlayerStatManager playerStatManager;

    private void Start()
    {
         TryGetComponent<PlayerStatManager>(out playerStatManager);
        if (GameManager.instance == null)
            return;

        playersMerch = GameManager.instance.merch;
        money = GameManager.instance.playerMoney;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            BuyNearbyMerch();
        }
    }

    private void FixedUpdate()
    {
        FindMerchNearby();
    }

    void FindMerchNearby() 
    {
        Collider[] col = Physics.OverlapSphere(transform.position + Vector3.up, pickUpRange, merchLayer);
        if (col.Length == 0)
        {
            SetNearbyMerch(null);
            return;
        }

        foreach (var item in col)
        {
            if (item.transform.parent.TryGetComponent<MerchBehaviour>(out MerchBehaviour merchInstance))
            {
                SetNearbyMerch(merchInstance);
                return;
            }
        }
    }

    void SetNearbyMerch(MerchBehaviour merchBehaviour) 
    {
        nearbyMerch = merchBehaviour;

        if (merchDisplay == null)
            return;

        merchDisplay.DisplayMerch(nearbyMerch);
    } 

    void BuyNearbyMerch() 
    {
        if (nearbyMerch == null)
            return;

        MerchBehaviour merchInstance = nearbyMerch;
        SetNearbyMerch(null);
        if (money < merchInstance.cost)
            return;

        money -= merchInstance.cost;
        GetMerchItem(merchInstance.merch);
        Destroy(merchInstance.gameObject);
    }

    public void GetMerchItem(MerchSO merch) 
    {
        playersMerch.Add(merch);
        if (playerStatManager != null)
            merch.OnGetMerch(playerStatManager);
    }

    public void LoseMerchItem(MerchSO merch) 
    {
        playersMerch.Remove(merch);
        if (playerStatManager != null)
            merch.OnLoseMerch(playerStatManager);
    }

    void CalculateStats() // calculate total stats given by the inventory, then relays it to player stat manager
    {

    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] float pickUpRange;
    [SerializeField] LayerMask merchLayer;
    public int money;

    [SerializeField] MerchDisplayUI merchDisplay;

    MerchBehaviour nearbyMerch;

    public PlayerStatManager playerStatManager;

    public delegate void ValueChange(int newValue);
    public ValueChange moneyChange;

    private void Start()
    {
         TryGetComponent<PlayerStatManager>(out playerStatManager);
        if (GameManager.instance == null)
            return;
        money = GameManager.instance.playerMoney;
        ChangeMoney(0);
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

        MerchBehaviour closeestMerch = null;
        float closeestDistance = 100;

        foreach (var item in col)
        {
            if (item.transform.parent.TryGetComponent<MerchBehaviour>(out MerchBehaviour merchInstance))
            {
                float distanceFromPlayer = Vector3.Distance(merchInstance.transform.position, transform.position);

                if(distanceFromPlayer < closeestDistance) 
                {
                    closeestDistance = distanceFromPlayer;
                    closeestMerch = merchInstance;
                }
            }
        }

        SetNearbyMerch(closeestMerch);
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

        ChangeMoney(-merchInstance.cost);
        InventoryUIManager.newItem = merchInstance.merch;
        InventoryUIManager.OpenInventory();
        Destroy(merchInstance.gameObject);
    }

    public void ChangeMoney(int amount) 
    {
        money += amount;

        if(moneyChange != null) 
        {
            moneyChange(money);
        }
    }

    void CalculateStats() // calculate total stats given by the inventory, then relays it to player stat manager
    {

    }
}

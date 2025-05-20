using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] float pickUpRange;
    [SerializeField] LayerMask merchLayer;
    [SerializeField] int money;

    [SerializeField] List<MerchSO> playersMerch = new List<MerchSO>();

    [SerializeField] MerchDisplayUI merchDisplay;

    MerchBehaviour nearbyMerch;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            BuyNearbyMerch();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.merch = playersMerch;
            GameManager.instance.EndDay();
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
        MerchBehaviour merchInstance = nearbyMerch;
        SetNearbyMerch(null);
        if (money < merchInstance.cost)
            return;

        money -= merchInstance.cost;
        playersMerch.Add(merchInstance.merch);
        Destroy(merchInstance.gameObject);
    }

    void CalculateStats() // calculate total stats given by the inventory, then relays it to player stat manager
    {

    }
}

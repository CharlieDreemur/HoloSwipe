using UnityEngine;

public class MerchBehaviour : MonoBehaviour
{
    public MerchSO merch;
    [HideInInspector] public int cost;
    public Transform tf;
    public string effectDescription;


    private void Start()
    {
        if(merch != null)
            SetMerch(merch);
    }



    public void SetMerch(MerchSO merch) 
    {
        this.merch = merch;

        cost = (int) (merch.cost * (1- PlayerStatManager.discount));
        GameObject merchInstance = Instantiate(merch.mesh, transform.position, Quaternion.identity, transform);
        merchInstance.GetComponent<MeshCollider>().convex = true;
        merchInstance.layer = LayerMask.NameToLayer("Merch");
        merchInstance.AddComponent(typeof(Rigidbody));
        merchInstance.GetComponent<Rigidbody>().isKinematic = true;
    }

    public string GetDescription() 
    {
        string description = "";

        foreach (var item in merch.effects)
        {
           description +=  item.GetEffectString();
            description += "<br>";
        }

        return description;
    }
}

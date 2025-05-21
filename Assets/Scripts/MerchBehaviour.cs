using UnityEngine;

public class MerchBehaviour : MonoBehaviour
{
    public MerchSO merch;
    [HideInInspector] public int cost;
    public Transform tf;


    private void Start()
    {
        if(merch != null)
            SetMerch(merch);
    }



    public void SetMerch(MerchSO merch) 
    {
        this.merch = merch;

        cost = merch.cost;
        GameObject merchInstance = Instantiate(merch.mesh, transform.position, Quaternion.identity, transform);
        merchInstance.GetComponent<MeshCollider>().convex = true;
        merchInstance.layer = LayerMask.NameToLayer("Merch");
        merchInstance.AddComponent(typeof(Rigidbody));
        merchInstance.GetComponent<Rigidbody>().isKinematic = true;
    }
}

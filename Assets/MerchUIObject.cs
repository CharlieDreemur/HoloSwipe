using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MerchUIObject : MonoBehaviour
{
    public MerchSO merch;
    [SerializeField] Image merchPosDisplay;
    public List<InventoryTile> inventoryTiles = new List<InventoryTile>();

    private void Start()
    {
        GameObject merchMesh = Instantiate(merch.mesh, transform.position, Quaternion.identity, transform);
        merchMesh.transform.localScale = Vector3.one * merch.UISize;
        merchMesh.layer = LayerMask.NameToLayer("UI");

        foreach (var item in merch.merchUIShape)
        {
            Image displayInstance = Instantiate(merchPosDisplay);
            displayInstance.transform.parent = transform;

            displayInstance.transform.position = merchMesh.transform.position + Vector3.back + ( new Vector3(item.x, item.y,0) * 0.9f) + (Vector3)merch.zeroZeroPos;
            displayInstance.transform.localScale = Vector3.one / 35;
            Color c = Color.blue;
            c.a = 0.25f;
            displayInstance.color = c;
        }
    }

    public void ClearInventoryTiles() 
    {
        foreach (var item in inventoryTiles)
        {
            item.merchUIHere = null;
        }
        inventoryTiles.Clear();
    }
}

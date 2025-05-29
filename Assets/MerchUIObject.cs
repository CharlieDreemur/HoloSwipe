using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MerchUIObject : MonoBehaviour
{
    public MerchInstance merch;
    [SerializeField] Image merchPosDisplay;
    public List<InventoryTile> inventoryTiles = new List<InventoryTile>();

    public List<Vector2Int> tilePos = new List<Vector2Int>();
    public Vector2 offset;

    int rotIndex = 1;

    public bool TestObject = false;

    private void Awake()
    {
        if (!TestObject)
            return;

        if (merch == null)
            return;

        SetMerch(merch);
    }

    public void SetMerch(MerchInstance merchInstance) 
    {
        merch = merchInstance;

        GameObject merchMesh = Instantiate(merch.merch.mesh, transform.position, Quaternion.identity, transform);
        merchMesh.transform.localScale = Vector3.one * merch.merch.UISize;
        merchMesh.transform.localRotation = Quaternion.Euler(0, 0, 0);
        merchMesh.layer = LayerMask.NameToLayer("UI");
        tilePos = new List<Vector2Int>(merch.merch.merchUIShape);
        offset = merch.merch.zeroZeroPos;

        foreach (var item in merch.merch.merchUIShape)
        {
            Image displayInstance = Instantiate(merchPosDisplay);
            displayInstance.transform.parent = transform;

            displayInstance.transform.position = merchMesh.transform.position + Vector3.back + (new Vector3(item.x, item.y, 0) * 0.9f) + (Vector3)offset;
            displayInstance.transform.localScale = Vector3.one / 35;
            Color c = Color.blue;
            c.a = 0.25f;
            displayInstance.color = c;
        }
    }

    public void SetMerchPosAndRot(Vector2Int pos) 
    {
        merch.inventoryRotIndex = rotIndex;
        merch.inventoryPosition = pos;
    }

    public void RotateTile() 
    {
        transform.Rotate(new Vector3(0, 0, -90));

        List<Vector2Int> newTilePos = new List<Vector2Int>();

        rotIndex++;
        if (rotIndex > 4)
            rotIndex = 1;

        foreach (var item in tilePos)
        {
            Vector2Int newPos = item;

            int xPos = (0 * item.x) + (1 * item.y);
            int yPos = (-1 * item.x) + (0 * item.y);
            newPos.x = xPos;
            newPos.y = yPos;
            newTilePos.Add(newPos);
        }


        float offsetXPos = (0 * offset.x) + (1 * offset.y);
        float offsetYPos = (-1 * offset.x) + (0 * offset.y);
        offset = new Vector2(offsetXPos, offsetYPos);
        tilePos = newTilePos;
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

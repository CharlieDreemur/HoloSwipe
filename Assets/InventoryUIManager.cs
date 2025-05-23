using UnityEngine;
using System.Collections.Generic;

public class InventoryUIManager : MonoBehaviour
{
    const int gridSize = 10;
    const float gizmosGridSize = .9f;

    const int gridSkip = 2;

    public InventoryTile[,] grid = new InventoryTile[gridSize, gridSize];

    public MerchUIObject heldMerchUIObject;

    [SerializeField] Transform UITileParent;
    [SerializeField] InventoryTile gridUiTile;

    [SerializeField] Camera cam;

    [SerializeField] LayerMask UILayer;

    List<InventoryTile> highLightedTiles = new List<InventoryTile>();

    private void Start()
    {
        PrepareGrid();
    }

    void PrepareGrid() 
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                bool isInRange = !(i < gridSkip || i >= gridSize - gridSkip || j < gridSkip || j >= gridSize - gridSkip);

                if (isInRange) 
                {
                    grid[i, j] = Instantiate(gridUiTile, (Vector3)GetGridWorldPosFromCords(new Vector2Int(i, j)), Quaternion.identity, UITileParent.transform);
                    grid[i, j].pos = new Vector2Int(i, j);
                }
            }
        }
    }

    

    public Vector3 GetGridWorldPosFromCords(Vector2Int cords) 
    {
        Vector3 pos = (new Vector2(cords.x * gizmosGridSize, cords.y * gizmosGridSize) + Vector2.one * gizmosGridSize / 2) + (Vector2)transform.position;
        pos.z = transform.position.z;
        return pos;
    }

    public Vector2Int GetCordsFromWorldPos(Vector3 worldPos) 
    {
        Vector2Int pos = Vector2Int.zero;

        Vector3 distanceFromTransform = worldPos - transform.position;
        while((distanceFromTransform.x < 0 || distanceFromTransform.x > gizmosGridSize + 0.1f)) 
        {
            if(distanceFromTransform.x < 0) 
            {
                distanceFromTransform += Vector3.right * gizmosGridSize;
                pos -= Vector2Int.right;
            }
            else 
            {
                distanceFromTransform += Vector3.left * gizmosGridSize;
                pos -= Vector2Int.left;
            }
        }
        
        while ((distanceFromTransform.y < 0 || distanceFromTransform.y > gizmosGridSize + 0.1f))
        {
            if (distanceFromTransform.y < 0)
            {
                distanceFromTransform += Vector3.up * gizmosGridSize;
                pos -= Vector2Int.up;
            }
            else
            {
                distanceFromTransform += Vector3.down * gizmosGridSize;
                pos -= Vector2Int.down;
            }
        }

        return pos;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        for (int i = 0; i < gridSize; i++)
        {
            Gizmos.DrawLine(pos, pos + Vector3.up * gridSize * gizmosGridSize);
            pos += Vector3.right * gizmosGridSize;
        }

        pos = transform.position;
        for (int i = 0; i < gridSize; i++)
        {
            Gizmos.DrawLine(pos, pos + Vector3.right * gridSize * gizmosGridSize);
            pos += Vector3.up * gizmosGridSize;
        }
    }

    public InventoryTile GetTileInPos(Vector2Int pos) 
    {
        if(!(pos.x > 0 && pos.x < gridSize)) 
        {
            return null;
        }

        if (!(pos.y > 0 && pos.y < gridSize))
            return null;

        return grid[pos.x, pos.y];
    }

    void HighlightTiles(List<InventoryTile> inventoryTiles) 
    {
        foreach (var item in highLightedTiles)
        {
            item.image.color = Color.white;
        }

        highLightedTiles.Clear();

        foreach (var item in inventoryTiles)
        {
            if (item == null)
                continue;
            if (item.merchUIHere != null)
                continue;
            item.image.color = Color.red;
            highLightedTiles.Add(item);
        }
    }

    private void Update()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);

        InventoryTile inventoryTile = GetTileInPos(GetCordsFromWorldPos(pos));

        if (heldMerchUIObject == null)
        {
           HighlightTiles(new List<InventoryTile> { inventoryTile });
        }
        else
        {
            List<InventoryTile> list = new List<InventoryTile>();
            foreach (var item in heldMerchUIObject.merch.merchUIShape)
            {
                list.Add(GetTileInPos(GetCordsFromWorldPos(pos) + item));
            }
            HighlightTiles(list);
        }
        

        if (heldMerchUIObject != null) 
        {

            pos -= (Vector3)heldMerchUIObject.merch.zeroZeroPos;
            pos.z = 500; 
            heldMerchUIObject.transform.position =pos;
            heldMerchUIObject.transform.localPosition = new Vector3(heldMerchUIObject.transform.localPosition.x, heldMerchUIObject.transform.localPosition.y, 0);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            if(heldMerchUIObject == null)
                ClickInArea();
            else 
            {
                if(inventoryTile != null) 
                {
                    if(DoesMerchItemFit(heldMerchUIObject.merch, inventoryTile.pos)) 
                    {
                        foreach (var item in heldMerchUIObject.merch.merchUIShape)
                        {
                            Vector2Int cords = inventoryTile.pos + item;
                            heldMerchUIObject.inventoryTiles.Add(grid[cords.x, cords.y]);
                            grid[cords.x, cords.y].merchUIHere = heldMerchUIObject;
                        }
                            heldMerchUIObject.transform.position = GetGridWorldPosFromCords(inventoryTile.pos) - (Vector3)heldMerchUIObject.merch.zeroZeroPos;
                    }
                    else
                    {
                        heldMerchUIObject.transform.localPosition = Vector3.zero;
                    }
                }

                heldMerchUIObject = null;
            }
        }
    }

    bool DoesMerchItemFit(MerchSO merch, Vector2Int pos) 
    {
        foreach (var item in merch.merchUIShape)
        {
            InventoryTile tileInPos = GetTileInPos(item + pos);

            if (tileInPos == null) 
            {
                return false;
            }
            if (tileInPos.merchUIHere != null)
                return false;
        }
        return true;
    }

    void ClickInArea() 
    {
        Vector3 beamPos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit ray;

        if( Physics.Raycast(beamPos, Vector3.forward, out ray,Mathf.Infinity, UILayer)) 
        {
            print(ray.collider.gameObject.name);
            heldMerchUIObject = ray.collider.transform.parent.GetComponent<MerchUIObject>();
            heldMerchUIObject.ClearInventoryTiles();
        }

    }
}

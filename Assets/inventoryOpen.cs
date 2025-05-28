using UnityEngine;

using UnityEngine.SceneManagement;

public class inventoryOpen : MonoBehaviour
{
    bool inv = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            int i;
            inv = false;
            for (i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == "InventoryScene")
                {
                    inv = true;
                    break;
                }
            }
            if (inv)
            {
                GameObject.FindAnyObjectByType<InventoryUIManager>().CloseInventory();
            } else
            {
                InventoryUIManager.OpenInventory();
            }
        }
    }
}

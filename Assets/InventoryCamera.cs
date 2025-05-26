using UnityEngine;

public class InventoryCamera : MonoBehaviour
{
    GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        gameObject.transform.position = mainCamera.transform.position;
        gameObject.transform.rotation = mainCamera.transform.rotation;
    }

}

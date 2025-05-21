using UnityEngine;

public class LeaveBounds : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatManager statManager = other.gameObject.GetComponent<PlayerStatManager>();
            PlayerInventory inventory = other.gameObject.GetComponent<PlayerInventory>();

            GameManager.instance.EndDay(inventory, statManager);
        }
    }
}

using UnityEngine;

public class LeaveBounds : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.EndDay();
        }
    }
}

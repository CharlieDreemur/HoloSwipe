using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] Transform playerloc;
    [SerializeField] GameObject ParadoxFilter; 

    public float minX, maxX, minZ, maxZ;
    public float angle = 60; // yeah nah just gotta change both values i aint dealing with quaternions 
    public float height;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerloc.position.x, playerloc.position.y + height, playerloc.position.z - height / Mathf.Tan(angle * Mathf.PI/180));
        Mathf.Clamp(transform.position.x, minX, maxX);
        Mathf.Clamp(transform.position.z, minZ, maxZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TimeParadox"))
        {
            //ParadoxFilter.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TimeParadox"))
        {
            ParadoxFilter.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TimeParadox"))
        {
            ParadoxFilter.SetActive(false);
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TimeParadox"))
        {
            ParadoxFilter.SetActive(true);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("TimeParadox"))
        {
            ParadoxFilter.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("TimeParadox"))
        {
            //ParadoxFilter.SetActive(false);
        }
    }*/
}

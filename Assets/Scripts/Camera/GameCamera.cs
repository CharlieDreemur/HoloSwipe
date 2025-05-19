using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] Transform playerloc;
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
}

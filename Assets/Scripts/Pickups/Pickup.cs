using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] GameObject PlayerCharacter;

    [SerializeField] protected float lifetime; //how long the pickup lasts 

    private float timePassed = 0;
    public Vector3 playerloc()
    {
        return new Vector3(PlayerCharacter.transform.position.x, 0, PlayerCharacter.transform.position.z);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] public GameManager gm;
    [SerializeField] public GameObject playerCharacter;

    [SerializeField] protected int value;
    [SerializeField] protected float pickupRadius;
    [SerializeField] protected float pickupSpeed;
    [SerializeField] protected float pickupAccel;
    [SerializeField] protected float lifetime; //how long the pickup lasts 


    public bool magneted = false; // whether the player entered pickup radius
    public bool touchingPlayer;

    protected bool slowed;
    private float timePassed = 0;
    
    public Vector3 playerloc()
    {
        return new Vector3(playerCharacter.transform.position.x, 0, playerCharacter.transform.position.z);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        
        if (Vector3.Distance(gameObject.transform.position, playerloc()) < pickupRadius)
        {
            magneted = true;
        }
        if (magneted)
        {
            pickupSpeed += pickupAccel * Time.deltaTime;
            gameObject.transform.position += (playerloc() - gameObject.transform.position).normalized * pickupSpeed * Time.deltaTime;
        }
        if (touchingPlayer)
        {
            int newValue = value + (int)PlayerStatManager.pickUpBonus;
            newValue = Mathf.RoundToInt( newValue * PlayerStatManager.pickUpMulti);

            playerCharacter.GetComponent<PlayerInventory>().ChangeMoney(value);
            MoneyChanges.instance.GainMoney(value);
            GlobalAudio.instance.CollectSound();
            Destroy(gameObject);
            //give them stuff
        }
        if (timePassed > lifetime)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
        if (other.gameObject.CompareTag("TimeParadox"))
        {
            slowed = true;
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
        if (other.gameObject.CompareTag("TimeParadox"))
        {
            slowed = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
        if (other.gameObject.CompareTag("TimeParadox"))
        {
            slowed = false;
        }
    }
}

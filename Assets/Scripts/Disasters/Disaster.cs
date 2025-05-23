using UnityEngine;

public class Disaster : MonoBehaviour
{
    public int day;
    
    //[SerializeField] GameManager gm; //This is where I would put the GameManager, IF I HAD ONE
    [SerializeField] public GameObject playerCharacter;
    [SerializeField] protected float speedIncrease; // this is how much faster this disaster gets each day, mostly for ones who chase you, but can potentially be used to shorten windup

    public bool touchingPlayer;

    protected bool slowed;

    public Vector3 playerloc()
    {
        return new Vector3(playerCharacter.transform.position.x, 0, playerCharacter.transform.position.z);
    }


    public void EndDay() //general method disasters use to end the day if they hit the player
    {
        print("Day ended frfr");
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }*/

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

using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] StatsSO BaseStats;
    [SerializeField] ECM2.Character PlayerCharacter;
    [SerializeField] GameObject Player;
    public float speedMultiplier = 1, salaryMultiplier = 1, discount = 1, luck = 0, fanScoreMultiplier = 1, fanScore = 0;
    public PlayerStats playerStats;

    public bool slowed;

    private void Start()
    {
        //the way im gonna do it now is very scuffed and needs to be looked at again later
        if(GameManager.instance != null)
            playerStats = new PlayerStats( GameManager.instance.playerStats);
        else
            playerStats = new PlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCharacter.rotationRate = BaseStats.baseRotationSpeed * speedMultiplier;
        PlayerCharacter.maxWalkSpeed = (BaseStats.baseMoveSpeed + playerStats.speed) * speedMultiplier;
        if (slowed)
        {
            PlayerCharacter.rotationRate = PlayerCharacter.rotationRate * 0.3f;
            PlayerCharacter.maxWalkSpeed = PlayerCharacter.maxWalkSpeed * 0.3f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TimeParadox"))
        {
            slowed = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TimeParadox"))
        {
            slowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TimeParadox"))
        {
            slowed = false;
        }
    }
}

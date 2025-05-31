using UnityEngine;
using System.Collections;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] StatsSO BaseStats;
    [SerializeField] ECM2.Character PlayerCharacter;
    [SerializeField] GameObject Player;
    public static float speedMultiplier = 1, salary = 1, discount, luck = 0, fanScoreMultiplier = 1, fanScore = 0, baseSalary = 100, conTime = 0, pickUpBonus = 0, pickUpMulti = 1;
    public PlayerStats playerStats;

    public static void ResetStats()
    {
        speedMultiplier = 1; salary = 1; discount = 0; luck = 0; fanScoreMultiplier = 1; fanScore = 0; baseSalary = 100; conTime = 0; pickUpBonus = 0; pickUpMulti = 1;
    }

    public bool slowed;

    public bool steal = true;
    public void DisableSteal(int value)
    {
        if (steal)
        {
            MoneyChanges.instance.LoseMoney(value);
            gameObject.GetComponent<PlayerInventory>().ChangeMoney(-1 * value);
            StartCoroutine(DisableStealHelper());
        }

    }

    public IEnumerator DisableStealHelper()
    {
        steal = false;
        yield return new WaitForSeconds(1);
        steal = true;
    }


    

    private void Start()
    {
        //the way im gonna do it now is very scuffed and needs to be looked at again later
        if (GameManager.instance != null)
            playerStats = new PlayerStats( GameManager.instance.playerStats);
        else
            playerStats = new PlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCharacter.rotationRate = BaseStats.baseRotationSpeed * speedMultiplier;
        PlayerCharacter.maxWalkSpeed = (BaseStats.baseMoveSpeed) * speedMultiplier;
        if (slowed)
        {
            PlayerCharacter.rotationRate = PlayerCharacter.rotationRate * 0.7f;
            PlayerCharacter.maxWalkSpeed = PlayerCharacter.maxWalkSpeed * 0.7f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TimeParadox"))
        {
            if (!slowed)
            {
                GlobalAudio.instance.enterTimeSlow();
            }
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
            StartCoroutine(exitTimeParadoxAudio());
            slowed = false;
           
        }
    }

    public IEnumerator exitTimeParadoxAudio()
    {
        yield return new WaitForFixedUpdate();


        if (!slowed)
        {
            GlobalAudio.instance.exitTimeSlow();
        }
        //GlobalAudio.instance.exitTimeSlow();

    }
}

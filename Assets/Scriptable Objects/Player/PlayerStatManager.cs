using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] StatsSO BaseStats;
    [SerializeField] ECM2.Character PlayerCharacter;
    public float speedMultiplier = 1, salaryMultiplier = 1, discount = 1, LuckMultiplier = 1, fanScoreMultiplier = 1, fanScore = 0; 

    // Update is called once per frame
    void Update()
    {
        PlayerCharacter.rotationRate = BaseStats.baseRotationSpeed * speedMultiplier;
        PlayerCharacter.maxWalkSpeed = BaseStats.baseMoveSpeed * speedMultiplier;
    }

    
}

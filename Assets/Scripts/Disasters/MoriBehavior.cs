using UnityEngine;
using UnityEngine.AI;

public class MoriBehavior : Disaster
{
    [SerializeField] NavMeshAgent self; // some disasters use this to chase the player
    
    void Update()
    {
        
    }
}

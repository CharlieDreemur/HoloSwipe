using UnityEngine;
using UnityEngine.AI;

public class Chaser : Disaster
{
    [SerializeField] protected NavMeshAgent agent;

    protected void Update()
    {
        agent.SetDestination(playerloc());
        if (touchingPlayer)
        {
            stealMoney(2);
        }
    }
}

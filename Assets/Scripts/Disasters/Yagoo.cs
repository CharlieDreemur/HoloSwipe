using UnityEngine;
using UnityEngine.AI;

public class Yagoo : Chaser
{
    public float baseSpeed;
    public float baseRotSpeed; //540 is fine
    public float baseAcceleration; //I think ~ 50 or so player can still juke out Mori by running circles even at base speed
    public float YagooAccel; // how much faster yagoo gets every second.

    private float YagooFactor = 1; 
    //private float timePassed = 0;

    private void Start()
    {
        agent.speed = baseSpeed;
        agent.angularSpeed = baseRotSpeed;
        agent.acceleration = baseAcceleration;

    }
    new private void Update()
    {
        
        if (slowed)
        {
            agent.speed = baseSpeed * YagooFactor*0.7f;
            agent.angularSpeed = baseRotSpeed * YagooFactor * 0.7f;
            agent.acceleration = baseAcceleration * YagooFactor*0.7f;

            YagooFactor += YagooAccel * Time.deltaTime * 0.7f;
        }
        else {
            agent.speed = baseSpeed * YagooFactor;
            agent.angularSpeed = baseRotSpeed * YagooFactor;
            agent.acceleration = baseAcceleration * YagooFactor;

            YagooFactor += YagooAccel * Time.deltaTime;
        }
        if (touchingPlayer)
        {
            stealMoney((int)(50 * (Mathf.Pow(1.25f, GameManager.instance.day))));
            EndDay();

        }
        base.Update();

    }

}

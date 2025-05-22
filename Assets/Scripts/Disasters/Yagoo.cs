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

    private void Awake()
    {
        agent.speed = baseSpeed;
        agent.angularSpeed = baseRotSpeed;
        agent.acceleration = baseAcceleration;

    }
    new private void Update()
    {
        base.Update();
        if (slowed)
        {
            agent.speed = baseSpeed * YagooFactor/3;
            agent.angularSpeed = baseRotSpeed * YagooFactor/3;
            agent.acceleration = baseAcceleration * YagooFactor/3;

            YagooFactor += YagooAccel * Time.deltaTime/3;
        }
        else {
            agent.speed = baseSpeed * YagooFactor;
            agent.angularSpeed = baseRotSpeed * YagooFactor;
            agent.acceleration = baseAcceleration * YagooFactor;

            YagooFactor += YagooAccel * Time.deltaTime;
        }
        
    }

}

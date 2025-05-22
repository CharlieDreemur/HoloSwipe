using UnityEngine;
using UnityEngine.AI;

public class MoriBehavior : Chaser
{
    public float baseSpeed;
    public float baseRotSpeed; //540 is fine
    public float baseAcceleration; //I think ~ 50 or so player can still juke out Mori by running circles even at base speed
    public float duration;
    public float exitDuration; //time it takes for the animation to play

    private float timePassed;

    private void Awake()
    {
        baseSpeed = baseSpeed * (1 + speedIncrease * day);
        baseRotSpeed = baseRotSpeed * (1 + speedIncrease * day);
        baseAcceleration = baseAcceleration * (1 + speedIncrease * day);
        agent.speed = baseSpeed;
        agent.angularSpeed = baseRotSpeed;
        agent.acceleration = baseAcceleration;
    }


    new private void Update()
    {
        if (slowed)
        {
            agent.speed = baseSpeed/3;
            agent.angularSpeed = baseRotSpeed/3;
            agent.acceleration = baseAcceleration/3;
            timePassed += Time.deltaTime/3;

        } else
        {
            agent.speed = baseSpeed;
            agent.angularSpeed = baseRotSpeed;
            agent.acceleration = baseAcceleration;
            timePassed += Time.deltaTime;
        }
        
        if (timePassed > duration)
        {
            if (timePassed > exitDuration+duration)
            {
                Destroy(gameObject);
            }
        } else
        {
            base.Update();
        }
    }
}

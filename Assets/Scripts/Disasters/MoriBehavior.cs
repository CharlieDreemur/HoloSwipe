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

    private void Start()
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
            agent.speed = baseSpeed * 0.7f;
            agent.angularSpeed = baseRotSpeed * 0.7f;
            agent.acceleration = baseAcceleration * 0.7f;
            timePassed += Time.deltaTime * 0.7f;

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

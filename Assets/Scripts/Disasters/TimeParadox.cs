using UnityEngine;

public class TimeParadox : Disaster
{
    public float duration;
    public float radius;

    [SerializeField] GameObject zone, ame;

    public float spawnTime; //how long it takes for the sphere to appear/disappear

    float timePassed = 0;
    private void Start()
    {
        
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed > duration + spawnTime * 1.95f)//a little faster to
        {
            if (ame)
            {
                Destroy(ame);
            }
            
        }

        if (timePassed > duration + spawnTime * 2.2f)// destroy later so it doesnt destroy before exit collision
        {
            Destroy(gameObject);
        }
        if (timePassed < spawnTime)
        {
            zone.transform.localScale = new Vector3(radius * timePassed/spawnTime, radius * timePassed / spawnTime, radius * timePassed / spawnTime);
        } else if (timePassed > duration + spawnTime)
        {
            zone.transform.localScale = new Vector3(radius * (1-(timePassed- spawnTime - duration) / spawnTime), radius * (1 - (timePassed - spawnTime - duration) / spawnTime), radius * (1 - (timePassed - spawnTime - duration) / spawnTime));
            if (timePassed > duration + spawnTime*2)
            {
                zone.transform.localScale = new Vector3(0, 0, 0);
            }
        } else
        {
            zone.transform.localScale = new Vector3(radius, radius, radius);
        }

    }

}

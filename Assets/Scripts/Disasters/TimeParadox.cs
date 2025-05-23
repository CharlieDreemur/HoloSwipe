using UnityEngine;

public class TimeParadox : Disaster
{
    public float duration;
    public float radius;

    [SerializeField] GameObject zone;

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
            Destroy(gameObject);
        }

        if (timePassed < spawnTime)
        {
            zone.transform.localScale = new Vector3(radius * timePassed/spawnTime, radius * timePassed / spawnTime, radius * timePassed / spawnTime);
        } else if (timePassed > duration + spawnTime)
        {
            zone.transform.localScale = new Vector3(radius * (1-(timePassed- spawnTime - duration) / spawnTime), radius * (1 - (timePassed - spawnTime - duration) / spawnTime), radius * (1 - (timePassed - spawnTime - duration) / spawnTime));
        } else
        {
            zone.transform.localScale = new Vector3(radius, radius, radius);
        }

    }

}

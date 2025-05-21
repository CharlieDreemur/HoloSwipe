using UnityEngine;

public class TimeParadox : Disaster
{
    public float duration;
    public float radius;

    [SerializeField] GameObject zone;

    public float spawnTime; //how long it takes for the sphere to appear/disappear

    float timePassed = 0;
    private void Awake()
    {
        
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (touchingPlayer)
        {
            playerCharacter.GetComponent<PlayerStatManager>().slowed = true;
        } else
        {
            playerCharacter.GetComponent<PlayerStatManager>().slowed = false;
        }

        if (timePassed > duration + spawnTime * 2)
        {
            Destroy(gameObject);
        }

        if (timePassed < spawnTime)
        {
            zone.transform.localScale = new Vector3(radius * timePassed/spawnTime, radius * timePassed / spawnTime, radius * timePassed / spawnTime);
        } else if (timePassed > duration + spawnTime)
        {
            zone.transform.localScale = new Vector3(radius * (1-(timePassed- spawnTime - duration) / spawnTime), radius * (1 - (timePassed - spawnTime - duration) / spawnTime), radius * (1 - (timePassed - spawnTime - duration) / spawnTime));
        }

    }

}

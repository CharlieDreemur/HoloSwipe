using UnityEngine;

public class Tentacle : Disaster
{
    public float duration;

    public float spawnTime;

    public float depth; // how low the sharks are at the start
    public float height; // their max height(starting from depth)


    private bool returning = false; // used to play the particle system again
    [SerializeField] GameObject go;
    [SerializeField] ParticleSystem dustCloud;


    float timePassed = 0;

    void Awake()
    {
        var main = dustCloud.main;
        main.duration = spawnTime;
        go.transform.position = new Vector3(go.transform.position.x, depth, go.transform.position.z);
        dustCloud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed > duration + spawnTime * 2 + 2)//give extra time for particles to despawn, should be underground anyways
        {
            Destroy(gameObject);
        }

        if (timePassed < spawnTime)
        {
           
            go.transform.position = new Vector3(go.transform.position.x, depth + height * timePassed / spawnTime, go.transform.position.z);
        }
        else if (timePassed > spawnTime + duration)
        {
            if (!returning)
            {
                returning = true;
                dustCloud.Play();
            }
            go.transform.position = new Vector3(go.transform.position.x, depth + height * (1 - (timePassed - spawnTime - duration) / spawnTime), go.transform.position.z);
        }
        else
        {
            go.transform.position = new Vector3(go.transform.position.x, depth + height, go.transform.position.z);
            if (touchingPlayer)
            {
                EndDay();
            }
        }


    }
}

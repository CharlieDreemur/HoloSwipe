using UnityEngine;

public class EldritchManager : Disaster
{
    public float duration; //these two are just used for calculations for special effects
    public float spawnTime;
    public float minDist; //minimum distance spawned from player

    public int numTentacles;


    public float minX, maxX, minZ, maxZ; //determine the bounds for spawning the tentacles

    public GameObject TentaclePrefab;
    public GameObject InaTentaclePrefab;
    public GameObject Camera;
    private float timePassed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera.GetComponent<CameraShake>().shake = spawnTime;
        GlobalAudio.instance.EarthShake();
        GameObject temp = Instantiate(InaTentaclePrefab);
        float x = 0;
        float z = 0;
        do //prevents tentacles from spawning directly on player
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(minZ, maxZ);
        } while (Vector3.Distance(playerloc(), new Vector3(x, 0, z)) < minDist);
        temp.transform.position = new Vector3(x, 0, z);
        temp.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));


        for (int i = 0; i < numTentacles-1; i++)
        {
            temp = Instantiate(TentaclePrefab);
            do //prevents tentacles from spawning directly on player
            {
                x = Random.Range(minX, maxX);
                z = Random.Range(minZ, maxZ);
            } while (Vector3.Distance(playerloc(), new Vector3(x, 0, z)) < minDist);
            

            temp.transform.position = new Vector3(x, 0, z);
            temp.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        }
    }
    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > spawnTime + duration)
        {
            GlobalAudio.instance.EarthShake();
            Camera.GetComponent<CameraShake>().shake = spawnTime;
            Destroy(gameObject);
        }
    }



}

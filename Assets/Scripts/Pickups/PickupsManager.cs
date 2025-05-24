using UnityEngine;

public class PickupsManager : MonoBehaviour
{
    public int day; // day #

    [SerializeField] GameManager gm; //This is where I would put the GameManager, IF I HAD ONE
    [SerializeField] GameObject PlayerCharacter; //will pass this onto disasters spawned
    [SerializeField] Canvas canvas;

    [SerializeField] float spawnTimeMin;
    [SerializeField] float spawnTimeMax;
    
    [SerializeField] float minDist; //minimum distance spawns from player


    [SerializeField] float baseLootCrateChance; //free random merch with modifier + gold?
    //if an object is spawned, 
    [SerializeField] float baseCoinChance; //3 gold
    [SerializeField] float baseCoinBagChance; //5 gold
    [SerializeField] float baseLitterChance; // 1 gold

    private float luck; //increases chance of pickups, + increases chance of stronger pickups

    [SerializeField] float minX, maxX, minZ, maxZ; //bounds of the spawn location

    private float nextSpawn; //how much time must pass before the next disaster spawns
    private float timePassed = 0; // how much time has passed since the last disaster


    [SerializeField] GameObject litterPrefab, coinPrefab, coinBagPrefab, lootCratePrefab; // reference prefabs so they can spawn

    public Vector3 playerloc()
    {
        return new Vector3(PlayerCharacter.transform.position.x, 0, PlayerCharacter.transform.position.z);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float luckMult = 1 + luck * 0.01f;
        nextSpawn = Random.Range(spawnTimeMin / (luckMult), spawnTimeMax / (luckMult));
    }

    // Update is called once per frame
    void Update()
    {
        float luckMult = 1 + luck * 0.01f;
        timePassed += Time.deltaTime;
        if (timePassed > nextSpawn)
        {
            spawnRandom();
            nextSpawn += Random.Range(spawnTimeMin / (luckMult), spawnTimeMax / (luckMult));
        }
    }

    void spawnRandom()
    {
        float choice = Random.Range(0, baseCoinBagChance + baseCoinChance + baseLitterChance + baseLootCrateChance);
        //odds are also modified by luck
    }

}

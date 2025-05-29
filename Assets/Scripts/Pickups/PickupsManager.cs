using UnityEngine;

public class PickupsManager : MonoBehaviour
{
    public int day; // day #

    [SerializeField] GameManager gm; //This is where I would put the GameManager, IF I HAD ONE
    [SerializeField] GameObject playerCharacter; //will pass this onto disasters spawned
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

    

    public GameObject phoenixRebirthWarning, eldritchRitualWarning, timeParadoxWarning, sharkAttackWarning, reaperHuntWarning, yagooWarning, lootWarning;

    [SerializeField] GameObject litterPrefab, coinPrefab, coinBagPrefab, lootCratePrefab; // reference prefabs so they can spawn

    [SerializeField] MerchSO[] merchPool;

    public MerchSO GetRandomMerch()
    {
        return merchPool[Random.Range(0, merchPool.Length)];
    }

    public Vector3 playerloc()
    {
        return new Vector3(playerCharacter.transform.position.x, 0, playerCharacter.transform.position.z);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        luck = PlayerStatManager.luck;
        float luckMult = 1 + luck * 0.01f;
        nextSpawn = Random.Range(spawnTimeMin / (luckMult), spawnTimeMax / (luckMult));
    }

    // Update is called once per frame
    void Update()
    {
        luck = PlayerStatManager.luck;
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
        float choice = Random.Range(0,   baseLitterChance + baseCoinChance + 0.3f * luck + baseCoinBagChance + 0.6f * luck +  baseLootCrateChance +0.1f*luck);
        //odds are also modified by luck
        if (choice < baseLitterChance)
        {
            //spawn litter
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            while (Vector3.Distance(new Vector3(x, 0, z), playerloc()) < minDist)
            {
                x = Random.Range(minX, maxX);
                z = Random.Range(minZ, maxZ);
            }
            GameObject temp = Instantiate(litterPrefab);
            temp.GetComponent<Pickup>().playerCharacter = playerCharacter;
            temp.GetComponent<Pickup>().gm = gm;
            temp.transform.position = new Vector3(x, 0, z);
            temp.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        } else if (choice < baseLitterChance + baseCoinChance + 0.3f * luck)
        {
            //spawn coin
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            while (Vector3.Distance(new Vector3(x, 0, z), playerloc()) < minDist)
            {
                x = Random.Range(minX, maxX);
                z = Random.Range(minZ, maxZ);
            }
            GameObject temp = Instantiate(coinPrefab);
            temp.GetComponent<Pickup>().playerCharacter = playerCharacter;
            temp.GetComponent<Pickup>().gm = gm;
            temp.transform.position = new Vector3(x, 0, z);
            temp.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        } else if (choice < baseLitterChance + baseCoinChance + 0.3f * luck + baseCoinBagChance + 0.6f * luck)
        {
            //spawn coinbag
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            while (Vector3.Distance(new Vector3(x, 0, z), playerloc()) < minDist)
            {
                x = Random.Range(minX, maxX);
                z = Random.Range(minZ, maxZ);
            }

            GameObject temp = Instantiate(coinBagPrefab);
            temp.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            temp.GetComponent<Pickup>().playerCharacter = playerCharacter;
            temp.GetComponent<Pickup>().gm = gm;
            temp.transform.position = new Vector3(x, 0, z);
        } else
        {
            //spawn lootCrate
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            while (Vector3.Distance(new Vector3(x, 0, z), playerloc()) < minDist)
            {
                x = Random.Range(minX, maxX);
                z = Random.Range(minZ, maxZ);
            }
            resetWarnings();
            lootWarning.SetActive(true);
            GameObject temp = Instantiate(lootCratePrefab);
            
            temp.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            temp.GetComponent<LootCrate>().merch = GetRandomMerch();
            temp.GetComponent<LootCrate>().playerCharacter = playerCharacter;
            temp.GetComponent<LootCrate>().gm = gm;
            temp.transform.position = new Vector3(x, 1.2f, z);
        }

    }

    MerchSO randMech() //maybe will make this eventually
    {
        return null; 
    }

    void resetWarnings()
    {
        phoenixRebirthWarning.SetActive(false);
        eldritchRitualWarning.SetActive(false);
        timeParadoxWarning.SetActive(false);
        sharkAttackWarning.SetActive(false);
        yagooWarning.SetActive(false);
        reaperHuntWarning.SetActive(false);
        lootWarning.SetActive(false);
    }
}

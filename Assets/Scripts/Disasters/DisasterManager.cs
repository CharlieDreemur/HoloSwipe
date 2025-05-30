using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    public int day; // day #

    //[SerializeField] GameManager gm; //This is where I would put the GameManager, IF I HAD ONE
    [SerializeField] GameObject PlayerCharacter; //will pass this onto disasters spawned
    [SerializeField] GameObject Camera; //can pass onto disasters
    [SerializeField] Canvas canvas; // can be used to give player warnings

    [SerializeField] float spawnTimeMin; 
    [SerializeField] float spawnTimeMax;
    [SerializeField] float spawnTimeDecrease; // how much faster disasters spawn as days pass, its 1 + spawnTimeInc
    [SerializeField] float minDist; //minimum distance spawns from player
    [SerializeField] float huntMaxDist; //max distance mori/yagoo can spawn from player
    [SerializeField] float rebirthMaxDist;
    public float dayLength; //can also grab this from Game Manager

    public float yagooReload; //how fast new yagoos spawn after the day ends
    private float nextYagoo = 0;

    public GameObject phoenixRebirthWarning, eldritchRitualWarning, timeParadoxWarning, sharkAttackWarning, reaperHuntWarning, yagooWarning, lootWarning;

    [SerializeField] float minX, maxX, minZ, maxZ; //bounds of the spawn location
    //We can give Mori/Yagoo set spawn locations cause otherwise its annoying to make sure theyre not in walls

    private float nextSpawn; //how much time must pass before the next disaster spawns
    private float timePassed = 0; // how much time has passed since the last disaster
    

    [SerializeField] GameObject phoenixRebirthPrefab, eldritchRitualPrefab, timeParadoxPrefab, sharkAttackPrefab, yagooPrefab, reaperHuntPrefab; // reference prefabs so they can spawn

    public Vector3 playerloc()
    {
        return new Vector3(PlayerCharacter.transform.position.x, 0, PlayerCharacter.transform.position.z);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayLength =  60 + PlayerStatManager.conTime;
        nextSpawn = Random.Range(spawnTimeMin/ (1 + spawnTimeDecrease * day), spawnTimeMax/ (1 + spawnTimeDecrease * day));
    }

    // Update is called once per frame
    void Update()
    {
        
        day = GameManager.instance.day; //need to be able to access day from game manager 
        timePassed += Time.deltaTime;
        if (timePassed > nextSpawn)
        {
            spawnRandom();
            nextSpawn += Random.Range(spawnTimeMin / (1 + spawnTimeDecrease * day), spawnTimeMax / (1 + spawnTimeDecrease * day)); 
        }
        if (timePassed > dayLength + nextYagoo)
        {
            if (nextYagoo == 0)
            {
                resetWarnings();
                yagooWarning.SetActive(true);
            }
            Yagoo();
            nextYagoo += yagooReload;
        }
    }

    void spawnRandom()
    {
        int choice = Random.Range(0,5);
        switch (choice)
        {
            case 0:
                PhoenixRebirth();
                break;
            case 1:
                TimeParadox();
                break;
            case 2:
                SharkAttack();
                break;
            case 3:
                ReaperHunt();
                break;
            case 4:
                EldritchRitual();
                break;

        }
    }

    void PhoenixRebirth() //spawns near player
    {
        float x = playerloc().x + Random.insideUnitCircle.x * rebirthMaxDist;
        float z = playerloc().z + Random.insideUnitCircle.y * rebirthMaxDist;
        resetWarnings();
        phoenixRebirthWarning.SetActive(true);
        GameObject temp = Instantiate(phoenixRebirthPrefab);
        temp.transform.position = new Vector3(x, 0, z);
        temp.GetComponent<Disaster>().playerCharacter = PlayerCharacter;
        temp.GetComponent<Disaster>().day = day;
    }

    void TimeParadox() //spawns totally randomly, but not near player, needs player passed in
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        while (Vector3.Distance(new Vector3(x, 0, z), playerloc()) < minDist) // extra large minimum distance cause its so big
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(minZ, maxZ);
        }
        resetWarnings();
        timeParadoxWarning.SetActive(true);
        GameObject temp = Instantiate(timeParadoxPrefab);
        temp.GetComponent<Disaster>().playerCharacter = PlayerCharacter;
        temp.transform.position = new Vector3(x, 0, z);
        temp.GetComponent<Disaster>().day = day;
    }

    void SharkAttack() //spawns centered around player
    {
        float x = playerloc().x;
        float z = playerloc().z;
        resetWarnings();
        sharkAttackWarning.SetActive(true);
        GameObject temp = Instantiate(sharkAttackPrefab);
        temp.transform.position = new Vector3(x, 0, z);
        temp.GetComponent<Disaster>().playerCharacter = PlayerCharacter;
        temp.GetComponent<Disaster>().day = day;
    }

    void ReaperHunt() //spawns near player, but not within a certain radius, needs player passed in
    {
        float x = playerloc().x + Random.insideUnitCircle.x * huntMaxDist;
        float z = playerloc().z + Random.insideUnitCircle.y * huntMaxDist;
        while (Vector3.Distance(new Vector3(x, 1, z), playerloc()) < minDist)
        {
            x = playerloc().x + Random.insideUnitCircle.x * huntMaxDist;
            z = playerloc().z + Random.insideUnitCircle.y * huntMaxDist;
        }
        resetWarnings();
        reaperHuntWarning.SetActive(true);
        GameObject temp = Instantiate(reaperHuntPrefab);
        temp.transform.position = new Vector3(x, 1, z);
        temp.GetComponent<Disaster>().playerCharacter = PlayerCharacter;
        temp.GetComponent<Disaster>().day = day;
    }

    

    void EldritchRitual() //spawns at exactly (0,0,0), needs camera + player passed in
    {
        resetWarnings();
        eldritchRitualWarning.SetActive(true);
        GameObject temp = Instantiate(eldritchRitualPrefab);
        temp.transform.position = new Vector3(0, 0, 0);
        temp.GetComponent<Disaster>().playerCharacter = PlayerCharacter;
        temp.GetComponent<EldritchManager>().Camera = Camera;
        temp.GetComponent<Disaster>().day = day;
    }

    void Yagoo() //spawns near player, but not within a certain radius, needs player passed in
    {
        float x = playerloc().x + Random.insideUnitCircle.x * huntMaxDist;
        float z = playerloc().z + Random.insideUnitCircle.y * huntMaxDist;
        while (Vector3.Distance(new Vector3(x, 1, z), playerloc()) < minDist)
        {
            x = playerloc().x + Random.insideUnitCircle.x * huntMaxDist;
            z = playerloc().z + Random.insideUnitCircle.y * huntMaxDist;
        }
        GameObject temp = Instantiate(yagooPrefab);
        temp.transform.position = new Vector3(x, 1, z);
        temp.GetComponent<Disaster>().playerCharacter = PlayerCharacter;
        temp.GetComponent<Disaster>().day = day;
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

    bool inBounds(Vector3 checkVector) //checks whether a vector is in the bounds of the map, 
    {
        if (checkVector.x > maxX)
        {
            return false;
        }
        if (checkVector.x < minX)
        {
            return false;
        }
        if (checkVector.z > maxZ)
        {
            return false;
        }
        if (checkVector.z < minZ)
        {
            return false;
        }
        return true;
    }
}

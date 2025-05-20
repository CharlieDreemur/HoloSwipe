using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    public int day; // day #

    //[SerializeField] GameManager gm; //This is where I would put the GameManager, IF I HAD ONE
    [SerializeField] GameObject PlayerCharacter; //will pass this onto disasters spawned

    [SerializeField] float spawnTimeMin; 
    [SerializeField] float spawnTimeMax;
    [SerializeField] float spawnTimeDecrease; // how much faster disasters spawn as days pass, its 1 + spawnTimeInc

    [SerializeField] float minX, maxX, minZ, maxZ; //bounds of the spawn location
    //We can give Mori/Yagoo set spawn locations cause otherwise its annoying to make sure theyre not in walls

    private float nextSpawn; //how much time must pass before the next disaster spawns
    private float timePassed = 0; // how much time has passed since the last disaster

    

    public Vector3 playerloc()
    {
        return new Vector3(PlayerCharacter.transform.position.x, 0, PlayerCharacter.transform.position.z);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawn = Random.Range(spawnTimeMin/ (1 + spawnTimeDecrease * day), spawnTimeMax/ (1 + spawnTimeDecrease * day));
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > nextSpawn)
        {

        }
    }
}

using UnityEngine;

public class SharkAttack : Disaster
{

    public float duration;
    public float radius;

    public float spawnTime;

    public float rotSpeed;
    //both of these are used to make sure both gura and the sharks are fully submerged before and after
    public float depth; // how low the sharks are at the start
    public float height; //their max height(starting from depth)

    [SerializeField] GameObject sharkCenter;

    [SerializeField] GameObject[] sharks;

    float timePassed = 0;
    
    void Awake()
    {
        rotSpeed = rotSpeed * (1 + speedIncrease * day);
        for (int i = 0; i < 8; i++) //only 8 sharks, not gonna support changing this just cause its kinda annoying
        {
            sharks[i].gameObject.transform.localPosition = sharks[i].gameObject.transform.localPosition.normalized * radius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        sharkCenter.transform.Rotate(new Vector3(0,rotSpeed * Time.deltaTime,0));
        if (timePassed > duration + spawnTime * 2)
        {
            Destroy(gameObject);
        }

        if (timePassed < spawnTime)
        {
            sharkCenter.transform.position = new Vector3(sharkCenter.transform.position.x, depth + height * timePassed/spawnTime, sharkCenter.transform.position.z);
        } else if (timePassed > spawnTime + duration) {
            sharkCenter.transform.position = new Vector3(sharkCenter.transform.position.x, depth + height * (1-(timePassed-spawnTime-duration) / spawnTime), sharkCenter.transform.position.z);
        } else
        {
            sharkCenter.transform.position = new Vector3(sharkCenter.transform.position.x, depth + height, sharkCenter.transform.position.z);
            if (touchingPlayer)
            {
                EndDay();
            }
        }

       
    }
}

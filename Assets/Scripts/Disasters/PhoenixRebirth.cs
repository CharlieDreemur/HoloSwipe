using UnityEngine;

public class PhoenixRebirth : Disaster
{
    public float delay;
    public float duration; // how long the flames stick around
    public float kiaraDuration; // how long kiara stays after rebirth
    public float radius;
    public float flameHeight;
    public float kiaraAscensionSpeed; //just made it speed instead of height cause thats how I initially programmed it and i dont wanna spend another minute

    bool audioplayed = false;

    [SerializeField] GameObject innerCircle, outerCircle, flames, kiara;

    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip fireClip;

    float timePassed = 0;
    
    void Start()
    {
        delay = delay / (1 + speedIncrease*day); // fires faster on later days
        duration = duration / (1 + speedIncrease * day); // duration also decreases cause its not really meant to be a duration disaster
        outerCircle.transform.localScale = new Vector3(radius, 0.01f, radius);
        flames.transform.localScale = new Vector3(radius, 0.5f, radius);

    }

    private void Update() //not using a coroutine so it runs on gametime (pause menu etc)
    {
        timePassed += Time.deltaTime;
        innerCircle.transform.localScale = new Vector3(radius * timePassed / delay, 0.015f, radius * timePassed / delay);
        
        if (timePassed < delay)
        {
            kiara.transform.Rotate(new Vector3(90 * Time.deltaTime / (delay), 0, 0));
        }

        if (timePassed > delay)
        {
            flames.SetActive(false);
            if (timePassed < delay + duration)
            {
                
                innerCircle.SetActive(false);
                outerCircle.SetActive(false);
                flames.SetActive(true);
                if (!audioplayed)
                {
                    audioplayed = true;
                    audiosource.volume = GlobalAudio.instance.sfxvol;
                    audiosource.PlayOneShot(fireClip);
                }
                flames.transform.position -= new Vector3(0, flameHeight * Time.deltaTime/delay, 0);
                if (touchingPlayer)
                {
                    stealMoney((int)(50 * (Mathf.Pow(1.25f, GameManager.instance.day))));
                }
            }
            if (timePassed > delay)
            {
                kiara.transform.position += new Vector3(0, kiaraAscensionSpeed*Time.deltaTime, 0); //takes flight
                kiara.transform.Rotate(new Vector3(0, 540 * Time.deltaTime, 0)); //takes flight
            }

            if (timePassed > delay + duration + kiaraDuration) //just 2 seconds for the flames to dissipate
            {
                Destroy(gameObject);
            }
        }
    }


}

using UnityEngine;

public class Disaster : MonoBehaviour
{
    public int day;

    //[SerializeField] GameManager gm; //This is where I would put the GameManager, IF I HAD ONE
    [SerializeField] GameObject PlayerCharacter;
    [SerializeField] float speedmult; // this is how much faster this disaster gets each day, mostly for ones who chase you, but can potentially be used to shorten windup



    public Vector3 playerloc()
    {
        return new Vector3(PlayerCharacter.transform.position.x, 0, PlayerCharacter.transform.position.z);
    }


    public void EndDay() //general method disasters use to end the day if they hit the player
    {
        
    }


}

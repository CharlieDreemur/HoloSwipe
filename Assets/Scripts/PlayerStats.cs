using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public float speed, salary;

    public PlayerStats(PlayerStats player) 
    {
        speed = player.speed;
        salary = player.salary;
    }

    public PlayerStats()
    {
        speed = 0;
        salary = 0;
    }
}
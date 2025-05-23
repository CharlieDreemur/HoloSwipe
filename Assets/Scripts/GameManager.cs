using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    int day = 1;
    public int neededValue = 10;
    public List<MerchInstance> merch;

    public int playerMoney;
    public int salary = 15;

    public PlayerStats playerStats = new PlayerStats();



    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ProgressDay() 
    {
        neededValue = (int)(neededValue * 1.5f);
        day++;
        playerMoney += salary;
    }
    
    public void EndDay(PlayerInventory inventory, PlayerStatManager playerStats) 
    {
        this.playerStats = new PlayerStats(playerStats.playerStats);
        SceneManager.LoadScene("EndOfRoundScoreboard",LoadSceneMode.Additive);
    }
}
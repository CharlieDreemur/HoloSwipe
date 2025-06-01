using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int day = 1;
    public int neededValue = 10;
    public List<MerchInstance> merch;

    public int playerMoney = 10;
    //public int salary = 15;

    public PlayerStats playerStats = new PlayerStats();

    public MerchCollcetion merchPool;

    private void Awake()
    {
        if (instance != null && SceneManager.GetActiveScene().name != "MainMenu") //game will reset on main menu
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }
    
    public void ProgressDay() 
    {
        
        day++;
        switch (day)
        {
            case 1:
                neededValue = 75;
                break;
            case 2:
                neededValue = 150;
                break;
            case 3:
                neededValue = 250;
                break;
            case 4:
                neededValue = 400;
                break;
            case 5:
                neededValue = 700;
                break;
            case 6:
                neededValue = 1150;
                break;
            case 7:
                neededValue = 1750;
                break;

        }
        playerMoney += (int)PlayerStatManager.salary;
    }
    
    public void EndDay(PlayerInventory inventory, PlayerStatManager playerStats) 
    {
        this.playerStats = new PlayerStats(playerStats.playerStats);
        playerMoney = inventory.money;
        SceneManager.LoadScene("EndOfRoundScoreboard");
    }

    
}
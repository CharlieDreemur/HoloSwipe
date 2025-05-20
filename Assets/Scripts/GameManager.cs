using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    int day = 1;
    public int neededValue = 10;
    public List<MerchSO> merch;

    public int playerMoney;
    public int salary = 15;

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
    
    public void EndDay() 
    {
        SceneManager.LoadScene("EndOfRoundScoreboard");
    }
}

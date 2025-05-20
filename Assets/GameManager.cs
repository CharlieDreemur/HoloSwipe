using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    int day;
    int neededValue;
    public List<MerchSO> merch;

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
    
    public void EndDay() 
    {
        SceneManager.LoadScene("EndOfRoundScoreboard");
    }
}

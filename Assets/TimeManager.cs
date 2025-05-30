using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public int CurrTime;

    public delegate void PlayerValueChange(int newValue);
    public int RemainTime = 60;
    public PlayerValueChange timeChange;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        CurrTime = RemainTime + (int)PlayerStatManager.conTime;
        if (timeChange != null)
            timeChange(CurrTime);
        StartCoroutine(CountDownTimer());
    }

    IEnumerator CountDownTimer() 
    {
        while (true) 
        {
            CurrTime--;
            yield return new WaitForSeconds(1);

            if (timeChange != null)
                timeChange(CurrTime);

            if (CurrTime <= 0)
                break;
        }
    }


}

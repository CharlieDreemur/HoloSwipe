using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public int time;

    public delegate void PlayerValueChange(int newValue);
    public PlayerValueChange timeChange;

    void Start()
    {
        time = 1 + (int)PlayerStatManager.conTime;
        if (timeChange != null)
            timeChange(time);
        StartCoroutine(CountDownTimer());
    }

    IEnumerator CountDownTimer() 
    {
        while (true) 
        {
            time--;
            yield return new WaitForSeconds(1);

            if (timeChange != null)
                timeChange(time);

            if (time <= 0)
                break;
        }
    }


}

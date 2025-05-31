using UnityEngine;
using TMPro;
public class StatsDisplay : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI fanScore, speedMult, salary, discount, luck, contime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fanScore.text = "" + (int)PlayerStatManager.fanScore;
        speedMult.text = "+" + (int)((PlayerStatManager.speedMultiplier-1)*100) + "%";
        salary.text = "" + (int)PlayerStatManager.salary + "";
        discount.text = "-" + (int)(PlayerStatManager.discount*100) + "%";
        luck.text = "" + (int)PlayerStatManager.luck + "";
        contime.text = "+" + (int)PlayerStatManager.conTime + "s";
    }
}

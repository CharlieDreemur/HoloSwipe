using UnityEngine;
using TMPro;
public class StatsDisplay : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI fanScore, speedMult, salary, discount, luck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fanScore.text = "" + PlayerStatManager.fanScore;
        speedMult.text = "" + (PlayerStatManager.speedMultiplier-1)*100 + "%";
        salary.text = "" + PlayerStatManager.salary + "";
        discount.text = "-" + PlayerStatManager.discount*100 + "%";
        luck.text = "" + PlayerStatManager.luck + "";
    }
}

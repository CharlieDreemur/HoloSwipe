using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI timeText;


    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] TimeManager timeManager;

    private void Start()
    {
        moneyText.text = playerInventory.money.ToString();
        timeText.text = timeManager.CurrTime.ToString();
        

        playerInventory.moneyChange += ChangeMoney;
        timeManager.timeChange += ChangeTime;
    }

    public void ChangeMoney(int amount) 
    {
        moneyText.text = amount.ToString();
    }

    public void ChangeTime(int amount)
    {
        timeText.text = amount.ToString();
    }

}

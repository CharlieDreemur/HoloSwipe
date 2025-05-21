using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class EndOfRoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI boughtMerchText, valueText, nextValueText, moneyText;

    public int merchValue;

    private void Start()
    {
        DisplayBoghtMerch();
        valueText.text = "Total Value : " + CalculateValue() + "/" + GameManager.instance.neededValue;
        moneyText.text = "Player Money : " + GameManager.instance.playerMoney + "+" + GameManager.instance.salary;
        GameManager.instance.ProgressDay();
        nextValueText.text = "Next Day Value: " + GameManager.instance.neededValue; 
    }



    void DisplayBoghtMerch() 
    {
        string text = "Boght Merch:";

        List<MerchSO> merch = GameManager.instance.merch;

        foreach (var item in merch)
        {
            text += "<br> " + item.name;
        }

        boughtMerchText.text = text;
    }

    int CalculateValue() 
    {
        foreach (var item in GameManager.instance.merch)
        {
            item.OnEndEffect(this);
        }

        return merchValue;
    }

    public void GoBackToGame() 
    {
        SceneManager.LoadScene("Game Scene 1");
    }
}

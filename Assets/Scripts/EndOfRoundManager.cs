using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class EndOfRoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI boughtMerchText, valueText, nextValueText, moneyText;

    public int merchValue;
    bool madeValue;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        int playerValue = CalculateValue();
        madeValue = playerValue >= GameManager.instance.neededValue;

        DisplayBoghtMerch();
        valueText.text = "Total Value : " + playerValue + "/" + GameManager.instance.neededValue;
        moneyText.text = "Player Money : " + GameManager.instance.playerMoney + "+" + GameManager.instance.salary;
        GameManager.instance.ProgressDay();
        nextValueText.text = "Next Day Value: " + GameManager.instance.neededValue; 
    }



    void DisplayBoghtMerch() 
    {
        string text = "Boght Merch:";

        List<MerchInstance> merch = GameManager.instance.merch;

        foreach (var item in merch)
        {
            text += "<br> " + item.merch.name;
        }

        boughtMerchText.text = text;
    }

    int CalculateValue() 
    {
        foreach (var item in GameManager.instance.merch)
        {
            item.merch.OnEndEffect(this);
        }

        return merchValue;
    }

    public void Proceed() 
    {
        if(madeValue)
            SceneManager.LoadScene("PreCon");
        else
            SceneManager.LoadScene("GameOverScene");
    }
}

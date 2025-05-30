using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class EndOfRoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI boughtMerchText, valueText, nextValueText, moneyText;

    [SerializeField] string GameScene;

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
        moneyText.text = "Player Money : " + GameManager.instance.playerMoney + "+" + (int)PlayerStatManager.salary;
        GameManager.instance.ProgressDay();
        nextValueText.text = "Next Day Value: " + GameManager.instance.neededValue; 
    }



    void DisplayBoghtMerch() 
    {
        string text = "Boght Merch:";

        List<MerchInstance> merch = GameManager.instance.merch;

        foreach (var item in merch)
        {
            string merchName = item.merch.name;

            merchName = merchName.Replace("_MerchSO", "");

            text += "<br> " + merchName;
        }

        boughtMerchText.text = text;
    }

    int CalculateValue() 
    {
        return (int)PlayerStatManager.fanScore;
    }

    public void Proceed() 
    {
        if (madeValue)
        {
            if(GameManager.instance.day >= 7)
                SceneManager.LoadScene(GameScene);
            else
                SceneManager.LoadScene("WinScreen");
        }
        else
            SceneManager.LoadScene("GameOverScene");
    }
}

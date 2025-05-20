using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class EndOfRoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI boughtMerchText;

    private void Start()
    {
        DisplayBoghtMerch();
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

    public void GoBackToGame() 
    {
        SceneManager.LoadScene("Game Scene");
    }
}

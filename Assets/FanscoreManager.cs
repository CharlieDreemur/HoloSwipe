using UnityEngine;
using TMPro;

public class FanscoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fanscoreText;
    [SerializeField] GameManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fanscoreText.text = PlayerStatManager.fanScore + "/" + gm.neededValue;
    }
}

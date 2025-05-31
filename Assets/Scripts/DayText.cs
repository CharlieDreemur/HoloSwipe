using UnityEngine;
using TMPro;
public class DayText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayText.text = "Day:" + GameManager.instance.day;
    }

}

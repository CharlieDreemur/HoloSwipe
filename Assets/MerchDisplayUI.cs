using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MerchDisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI merchName, merchCost;
    [SerializeField] Image backGround;

    public void DisplayMerch(MerchBehaviour merch) 
    {
        if(merch == null) 
        {
            backGround.gameObject.SetActive(false);
            return;
        }

        backGround.gameObject.SetActive(true);
        merchName.text = merch.merch.name;
        merchCost.text = merch.cost.ToString();
    }
}

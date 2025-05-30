using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MerchDisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI merchName, merchCost, merchStats, merchFlavor;
    [SerializeField] Image merchInfo;
    [SerializeField] Camera cam;
    [SerializeField] Canvas canvas;
    [SerializeField] Vector3 displayOffset;
    public void DisplayMerch(MerchBehaviour merch)
    {        
        if(merch == null) 
        {
            merchInfo.gameObject.SetActive(false);
            return;
        }

        string merchName = merch.merch.name;

        merchName = merchName.Replace("_MerchSO", "");

        this.merchName.text = merchName;

        Vector3 temp = cam.WorldToScreenPoint(merch.tf.position);
        merchCost.text = merch.cost.ToString();
        merchInfo.transform.position = temp + displayOffset*canvas.scaleFactor;
        merchInfo.gameObject.SetActive(true);
        merchStats.text = merch.GetDescription();
        
    }

    public void DisplayMerch(MerchInstance merch)
    {
        if (merch == null)
        {
            merchInfo.gameObject.SetActive(false);
            return;
        }

        string merchName = merch.merch.name;

        merchName = merchName.Replace("_MerchSO", "");

        this.merchName.text = merchName;
        merchCost.text = "Cost:" + merch.merch.cost.ToString();
        merchInfo.gameObject.SetActive(true);
        merchStats.text = merch.GetDescription();

    }
}

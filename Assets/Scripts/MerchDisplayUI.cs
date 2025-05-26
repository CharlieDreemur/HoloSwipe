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

        Vector3 temp = cam.WorldToScreenPoint(merch.tf.position);
        merchName.text = merch.merch.name;
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

        merchName.text = merch.merch.name;
        merchCost.text = merch.merch.cost.ToString();
        merchInfo.gameObject.SetActive(true);
        merchStats.text = merch.GetDescription();

    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MerchDisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI merchName, merchCost, merchStats, merchFlavor;
    [SerializeField] Image merchInfo;
    [SerializeField] Camera cam;
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
        merchInfo.transform.position = temp + displayOffset;
        merchInfo.gameObject.SetActive(true);
        
        
    }
}

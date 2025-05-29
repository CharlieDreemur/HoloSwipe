using UnityEngine;
using TMPro;
public class ChangeMoneyText : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.color -= new Color (0,0,0, 0.5f) * Time.deltaTime;
        gameObject.transform.localPosition += new Vector3(0, 15,0) * Time.deltaTime;
        if (text.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

   
}

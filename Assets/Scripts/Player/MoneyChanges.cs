using UnityEngine;

public class MoneyChanges : MonoBehaviour
{
    public static MoneyChanges instance;

    [SerializeField] Canvas parentcanvas;
    [SerializeField] GameObject gainMoney, loseMoney;
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public Vector3 playerloc()
    {
        return new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }

    private void Start()
    {
        instance = this;
    }
    public void LoseMoney(int amt)
    {
        GameObject temp = Instantiate(loseMoney, parentcanvas.transform);
        Vector3 pos = cam.WorldToScreenPoint(playerloc());
        temp.GetComponent<ChangeMoneyText>().text.text = "-$" + amt;
        temp.transform.position = pos;
    }

    public void GainMoney(int amt)
    {
        GameObject temp = Instantiate(gainMoney, parentcanvas.transform);
        Vector3 pos = cam.WorldToScreenPoint(playerloc());
        temp.GetComponent<ChangeMoneyText>().text.text = "+$" + amt;
        temp.transform.position = pos;
    }
}

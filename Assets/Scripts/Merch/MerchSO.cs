using UnityEngine;

[CreateAssetMenu(fileName = "MerchSO", menuName = "Scriptable Objects/MerchSO")]
public class MerchSO : ScriptableObject
{
    public GameObject mesh;

    public int cost;

    public MerchEffect[] effects;

    public void OnGetMerch(PlayerStatManager playerStatManager) 
    {
        foreach (var item in effects)
        {
            item.OnPickUp(playerStatManager);
        }
    }

    public void OnLoseMerch(PlayerStatManager playerStatManager)
    {
        foreach (var item in effects)
        {
            item.OnDrop(playerStatManager);
        }
    }

    public void OnEndEffect(EndOfRoundManager endOfRoundManager) 
    {
        foreach (var item in effects)
        {
            item.EndScreenEffect(endOfRoundManager);
        }
    }
}

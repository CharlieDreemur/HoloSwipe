using UnityEngine;

[CreateAssetMenu(fileName = "SpeedMerchEffect", menuName = "Scriptable Objects/Merch Effect/SpeedMerchEffect")]
public class SpeedMerchEffect : MerchEffect
{
    public float speedAmount;

    public override void OnPickUp(PlayerStatManager playerStatManager)
    {
        playerStatManager.playerStats.speed += speedAmount;
    }

    public override void OnDrop(PlayerStatManager playerStatManager)
    {
        playerStatManager.playerStats.speed -= speedAmount;
    }
}

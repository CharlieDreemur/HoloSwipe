using UnityEngine;

public abstract class MerchEffect : ScriptableObject
{
    public virtual void OnPickUp(PlayerStatManager playerStatManager) 
    {
    
    }

    public virtual void OnDrop(PlayerStatManager playerStatManager)
    {

    }

    public virtual void EndScreenEffect(EndOfRoundManager endOfRoundManager) 
    {
        
    }
}

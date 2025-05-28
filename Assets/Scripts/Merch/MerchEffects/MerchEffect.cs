using UnityEngine;

public abstract class MerchEffect : ScriptableObject
{
    public int value;
    public virtual void OnPickUp(PlayerStatManager playerStatManager) 
    {
    
    }

    public virtual void OnDrop(PlayerStatManager playerStatManager)
    {

    }

    public virtual void EndScreenEffect(EndOfRoundManager endOfRoundManager) 
    {
        
    }

    public abstract string GetEffectString();
    public abstract int GetEffectType();
}

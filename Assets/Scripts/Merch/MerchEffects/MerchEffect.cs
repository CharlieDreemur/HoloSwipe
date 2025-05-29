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


    public virtual float GetStatValue() 
    {
        return 0;
    }
    public abstract string GetEffectString();
    public abstract int GetEffectType();
}

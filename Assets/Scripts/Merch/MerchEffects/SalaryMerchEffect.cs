using UnityEngine;

[CreateAssetMenu(fileName = "SalaryMerchEffect", menuName = "Scriptable Objects/Merch Effect/SalaryMerchEffect")]
public class SalaryMerchEffect : MerchEffect
{
    public int salaryAmount;

    public override string GetEffectString()
    {
        return "+" + salaryAmount + " salary";
    }

    public override float GetStatValue()
    {
        return salaryAmount;
    }

    public override int GetEffectType()
    {
        return 3;
    }
}

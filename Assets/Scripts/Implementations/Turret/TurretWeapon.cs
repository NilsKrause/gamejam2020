using UnityEngine;

public class TurretWeapon : MonoBehaviour, ITurretWeapon
{
    [SerializeField]
    private WeaponStats stats;

    public float GetRange()
    {
        return stats.range;
    }

    public int GetDamage()
    {
        int damage = stats.baseDamage;
        float multiplyer = stats.baseMultiplyer;

        foreach (WeaponType type in stats.types)
        {
            damage += type.baseDamageModifyer;
        }

        foreach (WeaponType type in stats.types)
        {
            multiplyer += type.damageMultiplyer;
        }

        return Mathf.RoundToInt(damage * multiplyer);
    }

}
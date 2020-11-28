using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "TurretWeapons/Stats", order = 0)]
public class WeaponStats : ScriptableObject
{
    public int baseDamage;
    public float baseMultiplyer;
    public WeaponType[] types;
    public float range;
    public float fireRate;
    public int magazineSize;
    public float reloadTime;
}

using UnityEngine;

[CreateAssetMenu(fileName = "TurretStats", menuName = "Turrets/Stats", order = 1)]
public class TurretStats : ScriptableObject
{
    public string turretName;
    public int cost;
    public bool buildable;
    public TurretType[] types;
    public ITurret[] upgrades;
    public ITurretWeapon weapon;
}
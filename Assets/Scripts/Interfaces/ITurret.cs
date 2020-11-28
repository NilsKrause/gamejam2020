using UnityEngine;

public interface ITurret
{
    ITurret[] GetUpgrades();
    int GetCost();
    ITurretWeapon GetWeapon();
    bool IsBuildable();
    GameObject GetNearestEnemy();
}
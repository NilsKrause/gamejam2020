using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponType", menuName = "TurretWeapons/Type", order = 1)]
public class WeaponType : ScriptableObject
{
    public float damageMultiplyer;
    public int baseDamageModifyer;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentStats", menuName = "Agents/Stats", order = 1)]
public class AgentStats : ScriptableObject
{
    public int health;
    public int damageOnDeath;
    public float speed;
    public float rotateSpeed;
}

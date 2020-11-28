using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelStats", menuName = "Level/Stats", order = 1)]
public class LevelStats : ScriptableObject
{
    public AgentStats agent;
    public int amount;
}

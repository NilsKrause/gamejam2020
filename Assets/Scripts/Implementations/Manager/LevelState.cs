using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityLists))]
public class LevelState : MonoBehaviour, ILevelManager
{
    // Represents the current life total of the player
    private int life;

    // Represents the current player gold
    private int gold;

    private EntityLists entityManager;
    
    public delegate void OnAgentHealthZero(Agent agent);
    public event OnAgentHealthZero OnAgentHealthZeroEvent;

    void Awake()
    {
        entityManager = GetComponent<EntityLists>();
    }

    public ITurret[] GetBuildableTurrrets()
    {
        List<ITurret> buildableTurrets = new List<ITurret>();

        foreach (var to in entityManager.GetTurrets())
        {
            var turret = to.GetComponent<ITurret>();
            if (turret.IsBuildable())
            {
                buildableTurrets.Add(turret);
            }
        }

        return buildableTurrets.ToArray();
    }

    public int GetCurrentGold()
    {
        return gold;
    }

    public int GetPlayerLife()
    {
        return life;
    }

    public void OnAgentDeath(Agent agent)
    {
        if (OnAgentHealthZeroEvent != null)
        {
            OnAgentHealthZeroEvent(agent);
        }
        
        Debug.Log("Agent died. Poor bastard.");
        agent.gameObject.SetActive(false);
    }

    public bool PurchaseTurret(ITurret turret) 
    {
        int turretCost = turret.GetCost();

        if (turretCost <= gold)
        {
            gold -= turretCost;
            return true;
        }

        return false;
    }
    
}

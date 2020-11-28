using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ITurretWeapon))]
public class Turret : MonoBehaviour, ITurret
{
    private ITurretWeapon weapon;
    [SerializeField]
    private TurretStats stats;
    private enum TurretState { Idle, SearchingForTarget, ShootingAtTarget }
    private GameObject currentTarget;
    private TurretState state;
    CharacterController turretController;
    private IEnumerator coroutine;

    void Awake()
    {
        weapon = GetComponent<ITurretWeapon>();
        turretController = GetComponent<CharacterController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("ok statging");
        StartCoroutine(Idle());
        //Debug.Log("finished starting");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private float GetCooldown()
    {
        return 0.2f;
    }

    private void Search()
    {
        //Debug.Log("searching...");
        state = TurretState.SearchingForTarget;
        // see if we can find a target
        GameObject target = GetNearestEnemy();

        if (target)
        {
            this.currentTarget = target;
            StartCoroutine(Shoot());
        }
        else
        {
            StartCoroutine(Idle());
        }
    }

    private IEnumerator Shoot()
    {
        state = TurretState.SearchingForTarget;
        float distance = Vector3.Distance(currentTarget.transform.position, transform.position);
        if (distance <= weapon.GetRange())
        {
            // shooooot him!! :D
            Debug.DrawLine(transform.position, currentTarget.transform.position, Color.red, 0.2f);
            currentTarget.GetComponent<IAgent>().DealDamage(weapon.GetDamage());
            yield return new WaitForSeconds(GetCooldown());
        }

        Search();
    }

    private IEnumerator Idle()
    {
        //Debug.Log("idleing...");
        yield return new WaitForSeconds(0.2f);
        Search();
    }

    public int GetCost()
    {
        int cost = stats.cost;
        float multiplyer = 0;

        foreach (TurretType type in stats.types)
        {
            cost += type.costModifyer;
        }

        foreach (TurretType type in stats.types)
        {
            multiplyer += type.costMultiplyer;
        }

        return Mathf.RoundToInt(cost * multiplyer);
    }

    public ITurret[] GetUpgrades()
    {
        return stats.upgrades;
    }

    public ITurretWeapon GetWeapon()
    {
        return stats.weapon;
    }

    public bool IsBuildable()
    {
        return stats.buildable;
    }

    public GameObject GetNearestEnemy ()
    {
        //Debug.Log("getting nearest enemy");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Agent");
        float nearestDistance = float.MaxValue;
        GameObject nearestEnemy = null;
        Vector3 position = transform.position;

        if (enemies == null) return null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, position);
            if (distance <= weapon.GetRange() && distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        //if (nearestEnemy) Debug.Log("nearest enemy: " + nearestEnemy.name);
        //else Debug.Log("no enemy found");

        return nearestEnemy;
    }
}

public interface ITurretState
{
    void RunState();
    void ChangeState(ITurretState newState);
}
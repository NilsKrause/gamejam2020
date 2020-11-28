using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Agent : MonoBehaviour, IAgent
{
    [SerializeField]
    private AgentStats stats;
    
    private List<GameObject> waypointObjects;

    private LevelState levelState;

    private int currentHealth;

    private readonly List<Waypoint> waypoints = new List<Waypoint>();
    
    private int targetWaypointIndex = 0;

    private bool reachedTarget = false;

    private void Start()
    {
        Debug.Log("Agent Spawned! Health: " + stats.health);
        levelState = FindObjectOfType<LevelState>();
        waypointObjects = new List<GameObject>();
        
        var allWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointObjects = allWayPoints.ToList();
        
        if (stats == null) { throw new Exception("No STATS object assigned!"); }
        if (waypointObjects.Count == 0) { throw new Exception("There are no waypoints for this agent!");}
        
        foreach (var wo in waypointObjects)
        {
            waypoints.Add(new Waypoint(wo.transform.position));
        }


        currentHealth = stats.health;
    }

    public void AddWaypoint(GameObject waypointObject)
    {
        waypoints.Add(waypointObject.GetComponent<Waypoint>());
    }

    private Waypoint GetCurrentWaypoint()
    {
        return waypoints[targetWaypointIndex];
    }

    private void Update()
    {
        if (waypoints.Count == 0) throw new Exception("0 waypoints?! The heck?");
    }

    private void FixedUpdate()
    {
        if (reachedTarget) {
            return;
        }

        MoveTowardsCurrentWaypoint();

        if (Vector3.Distance(transform.position, GetCurrentWaypoint().GetPos()) < 0.1f)
        {
            TargetNextWaypoint();
        }
    }

    private void MoveTowardsCurrentWaypoint()
    {        
        Vector3 direction = GetCurrentWaypoint().GetPos() - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.fixedDeltaTime * GetRotateSpeed());

        transform.Translate(Vector3.forward * (Time.fixedDeltaTime * GetSpeed()));
    }

    private void TargetNextWaypoint()
    {
        if (targetWaypointIndex + 1 >= waypoints.Count)
        {
            reachedTarget = true;
            return;
        }

        targetWaypointIndex += 1;
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        levelState.OnAgentDeath(this);
    }

    public AgentStats GetStats()
    {
        return stats;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetSpeed()
    {
        return stats.speed;
    }

    public float GetRotateSpeed()
    {
        return stats.rotateSpeed;
    }
}
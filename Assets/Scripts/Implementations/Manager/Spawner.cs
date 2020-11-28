using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer;
    private float spawnDelay = 2f;

    private GameObject weakEnemy;
    private GameObject strongEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        _entityLists = FindObjectOfType<EntityLists>();
    }

    public static EntityLists _entityLists;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay)
        {
            timer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var newEnemy = Instantiate(_entityLists.GetAgents().First(), transform.position, Quaternion.identity);
        var agent = newEnemy.GetComponent<Agent>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}

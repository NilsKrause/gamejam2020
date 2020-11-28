using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLists : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> turrets = new List<GameObject>();
    [SerializeField]
    private List<GameObject> agents = new List<GameObject>();
    public List<GameObject> GetTurrets()
    {
        return turrets;
    }

    public List<GameObject> GetAgents()
    {
        return agents;
    }
}

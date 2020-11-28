using UnityEngine;

public class Waypoint
{
    private readonly Vector3 position;
    
    public Waypoint(Vector3 pos)
    {
        position = pos;
    }

    public Vector3 GetPos()
    {
        return position;
    }
}
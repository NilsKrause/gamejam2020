using System;
using System.Collections.Generic;
using UnityEngine;


public enum EventSubscriptionType { Gui, BuildTile }

public class EventHandle : IEventHandle
{
    private IEventReciever reciever;
    private TouchEvent handler;
    private readonly EventSubscriptionType type;

    public EventHandle(IEventReciever reciever, TouchEvent handler, EventSubscriptionType type)
    {
        this.type = type;
        this.reciever = reciever;
        this.handler = handler;
    }

    public void Unsubscribe()
    {
        handler.Remove(type, this);
    }

    public void Notify(Tile selectedTile)
    {
        reciever.HandleTouchEvent(selectedTile);
    }
}

public class TouchEvent : MonoBehaviour, ITouchEvent
{
    List<EventHandle> guiHandles;
    List<EventHandle> buildTileHandles;

    private Camera mainCamera;

    void Awake ()
    {
        guiHandles = new List<EventHandle>();
        buildTileHandles = new List<EventHandle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray =  mainCamera.ScreenPointToRay(Input.mousePosition);

            Debug.Log("Mouse button released");
            //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

            RaycastHit hit;
            Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity);

            if (hit.collider)
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("BuildableTile"))
                {
                    buildTileHandles.ForEach((handle) => handle.Notify(hitObject.GetComponent<Tile>()));
                }
            }
        }
    }

    public IEventHandle Subscribe(IEventReciever reciever, EventSubscriptionType eventType)
    {
        EventHandle handle;

        switch (eventType)
        {
            case EventSubscriptionType.BuildTile:
                handle = new EventHandle(reciever, this, eventType);
                buildTileHandles.Add(handle);
                break;

            case EventSubscriptionType.Gui:
                handle = new EventHandle(reciever, this, eventType);
                buildTileHandles.Add(handle);
                break;

            default:
                throw new Exception("unknown event subscription type");
        }

        return handle;
    }

    public void Remove(EventSubscriptionType eventType, EventHandle handle)
    {
        switch (eventType)
        {
            case EventSubscriptionType.BuildTile:
                buildTileHandles.Remove(handle);
                break;

            case EventSubscriptionType.Gui:
                guiHandles.Remove(handle);
                break;

            default:
                throw new Exception("unknown event subscription type");
        }
    }
}

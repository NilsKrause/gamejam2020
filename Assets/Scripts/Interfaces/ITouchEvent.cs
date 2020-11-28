using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchEvent
{
    IEventHandle Subscribe(IEventReciever reciever, EventSubscriptionType eventType);
}

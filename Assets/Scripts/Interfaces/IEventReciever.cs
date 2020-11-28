using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventReciever
{
    void HandleTouchEvent(Tile selectedTile);
}

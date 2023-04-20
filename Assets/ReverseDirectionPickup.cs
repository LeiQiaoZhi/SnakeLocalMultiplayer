using System.Collections;
using System.Collections.Generic;
using _Scripts.GameEventSystem;
using UnityEngine;

public class ReverseDirectionPickup : Pickup
{
    public GameEvent reverseDirectionEvent;
    public override void ApplyEffect(GameObject snake)
    {
        base.ApplyEffect(snake);
        reverseDirectionEvent.Raise();
    }
}

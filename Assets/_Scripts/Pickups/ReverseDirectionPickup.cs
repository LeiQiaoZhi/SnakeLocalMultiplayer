using System.Collections;
using System.Collections.Generic;
using _Scripts.GameEventSystem;
using _Scripts.Managers;
using UnityEngine;

public class ReverseDirectionPickup : Pickup
{
    public GameEvent reverseDirectionEvent;
    public override void ApplyEffect(GameObject snake)
    {
        base.ApplyEffect(snake);
        reverseDirectionEvent.Raise();
        MessageManager.Instance.DisplayInfoMessage("Reverse Direction!", Color.magenta, 1f, transform.position);
    }
}

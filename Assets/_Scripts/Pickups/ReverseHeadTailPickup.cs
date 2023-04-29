using System.Collections;
using System.Collections.Generic;
using _Scripts.GameEventSystem;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class ReverseHeadTailPickup : Pickup
{
    [FormerlySerializedAs("reverseDirectionEvent")] public GameEvent reverseHeadTailEvent;
    public override void ApplyEffect(GameObject snake)
    {
        base.ApplyEffect(snake);
        reverseHeadTailEvent.Raise();
        MessageManager.Instance.DisplayInfoMessage("Reverse Head Tail!", Color.green, 1f, transform.position);
    }
}

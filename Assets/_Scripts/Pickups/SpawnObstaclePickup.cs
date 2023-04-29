using _Scripts.GameEventSystem;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Pickups
{
    public class SpawnObstaclePickup : Pickup
    {
        public override void ApplyEffect(GameObject snake)
        {
            base.ApplyEffect(snake);
            PickupManager.Instance.TurnRandomCellToObstacle();
        }
    }
}

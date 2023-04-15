using UnityEngine;

namespace _Scripts.Snake
{
    public class Fruit : Pickup
    {
        public override void ApplyEffect(GameObject snake)
        {
            snake.GetComponent<SnakeMovement>().Grow();
        }
    }
}

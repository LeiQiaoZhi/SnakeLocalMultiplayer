using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using _Scripts.Snake;
using UnityEngine;

public class GrowFivePickup : Pickup
{
    public int growTimes = 3;
    public override void ApplyEffect(GameObject snake)
    {
        var snakeMovement = snake.GetComponent<SnakeMovement>();
        var snakeInitializer = snake.GetComponent<SnakeInitializer>();
        for (int i = 0; i < growTimes; i++)
        {
            snakeMovement.Grow();
        }

        var message = $"{snake.name} grows {growTimes} times!";
        MessageManager.Instance.DisplayInfoMessage(message, snakeInitializer.GetColor(), 1f, transform.position);
    }
}

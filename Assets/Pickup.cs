using _Scripts.Helpers;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int x;
    public int y;

    public virtual void ApplyEffect(GameObject snake)
    {
        XLogger.Log(Category.Pickup, "Applying effect of " + name + " to " + snake.name);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeMovement))]
public class SnakeInput : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode topKey;
    public KeyCode downKey;

    public float tickInterval = 0.3f;

    private SnakeMovement snakeMovement;
    private Direction direction;

    // Start is called before the first frame update
    private void Start()
    {
        snakeMovement = GetComponent<SnakeMovement>();
        StartCoroutine(SnakeInputCoroutine());
    }

    private IEnumerator SnakeInputCoroutine()
    {
        while (true)
        {
            snakeMovement.Move(direction);
            yield return new WaitForSeconds(tickInterval);
        }
    }
}
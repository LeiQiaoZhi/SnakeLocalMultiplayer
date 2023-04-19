using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid;
using _Scripts.Snake;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// singleton class that manages and spawns pickups 
/// </summary>
public class PickupManager : MonoBehaviour
{
    [SerializeField] private float fruitSpawnInterval = 1f;
    [SerializeField] private GameObject fruitPrefab;
    private List<Pickup> pickups = new List<Pickup>();
    private GridSystem gridSystem;

    public static PickupManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gridSystem = FindObjectOfType<GridSystem>();
    }

    // Start is called before the first frame update
    public void StartSpawning()
    {
        StartCoroutine(SpawnFruits());
    }

    public bool IsOccupiedByPickup(Vector2Int position)
    {
        return pickups.Exists(pickup => pickup.x == position.x && pickup.y == position.y);
    }

    public Pickup GetPickup(Vector2Int position)
    {
        return pickups.Find(pickup => pickup.x == position.x && pickup.y == position.y);
    }

    public void ApplyPickup(Vector2Int position, GameObject snake)
    {
        var pickup = GetPickup(position);
        pickup.ApplyEffect(snake);
        pickups.Remove(pickup);
        Destroy(pickup.gameObject);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// coroutine that spawns fruits at random positions
    /// avoid positions already occupied by snakes or pickups
    /// </summary>
    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            var freePositions = SnakeGameManager.Instance.GetFreePositions();
            // filter out positions occupied by pickups
            freePositions.RemoveAll(IsOccupiedByPickup);
            
            if (freePositions.Count == 0)
            {
                yield return null;
            }
            else
            {
                // get random position and spawn
                var randomIndex = Random.Range(0, freePositions.Count);
                var randomPosition = freePositions[randomIndex];
                var pickup = Instantiate(fruitPrefab, (Vector2)randomPosition, Quaternion.identity)
                    .GetComponent<Pickup>();
                // set position
                pickup.x = randomPosition.x;
                pickup.y = randomPosition.y;
                pickup.transform.position = gridSystem.CellToWorldPosition(randomPosition.x, randomPosition.y);
                
                pickups.Add(pickup);
                yield return new WaitForSeconds(fruitSpawnInterval);
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid;
using _Scripts.Snake;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class PickupSpawnRecord
{
    public GameObject prefab;
    public int weight = 1;
}

/// <summary>
/// singleton class that manages and spawns pickups 
/// </summary>
public class PickupManager : MonoBehaviour
{
    [SerializeField] private float fruitSpawnInterval = 1f;
    [SerializeField] private GameObject fruitPrefab;
    public List<PickupSpawnRecord> pickupSpawnRecords;
    [SerializeField] private float pickupSpawnInterval = 1f;

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
        StartCoroutine(SpawnPickups());
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

    public Vector2Int? GetRandomPosition()
    {
        var freePositions = SnakeGameManager.Instance.GetFreePositions();
        // filter out positions occupied by pickups
        freePositions.RemoveAll(IsOccupiedByPickup);
        if (freePositions.Count == 0)
        {
            return null;
        }

        var randomIndex = Random.Range(0, freePositions.Count);
        var randomPosition = freePositions[randomIndex];
        return randomPosition;
    }

    public void TurnRandomCellToObstacle()
    {
        var randomPosition = GetRandomPosition().GetValueOrDefault(-Vector2Int.one);
        if (randomPosition.x <= 0) return;
        var cell = gridSystem.GetCell(randomPosition.x, randomPosition.y);
        cell.StartCoroutine(cell.TurnToObstacle(2f));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// coroutine that spawns pickups other than fruits at random positions
    /// avoid positions already occupied by snakes or pickups
    /// </summary>
    private IEnumerator SpawnPickups()
    {
        while (true)
        {
            // get random position and spawn
            var randomPosition = GetRandomPosition().GetValueOrDefault(-Vector2Int.one);

            if (randomPosition.x <= 0) continue;

            // pick random pickups based on weights
            var pickupPrefab = GetRandomPickupPrefab();

            var pickup = Instantiate(pickupPrefab, (Vector2)randomPosition, Quaternion.identity)
                .GetComponent<Pickup>();
            // set position
            pickup.x = randomPosition.x;
            pickup.y = randomPosition.y;
            pickup.transform.position = gridSystem.CellToWorldPosition(randomPosition.x, randomPosition.y);

            pickups.Add(pickup);
            yield return new WaitForSeconds(pickupSpawnInterval);
        }
    }

    private GameObject GetRandomPickupPrefab()
    {
        var prefabCandidates = new List<GameObject>();
        foreach (var spawnRecord in pickupSpawnRecords)
        {
            for (int i = 0; i < spawnRecord.weight; i++)
            {
                prefabCandidates.Add(spawnRecord.prefab);
            }
        }
        
        return prefabCandidates[Random.Range(0, prefabCandidates.Count)];
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
            // get random position and spawn
            var randomPosition = GetRandomPosition().GetValueOrDefault(-Vector2Int.one);

            if (randomPosition.x <= 0) continue;

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
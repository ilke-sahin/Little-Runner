using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject coinPrefab, enemyPrefab;

    private Transform playerTransform;
    private float spawnZ = -3.0f;
    private float tileLength = 7.0f;
    private float safeZone = 15.0f;
    private int amtTilesOnScreen = 9;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //spawn the desired amount of tiles on screen
        //at once in the start of the game
        for (int i = 0; i < amtTilesOnScreen; i++)
        {
            //for the first blocks spawn the plain tile
            if (i < 4)
                SpawnTile(0);
            else
                SpawnTile();

        }

    }

    // Update is called once per frame
    void Update()
    {
        //calculates if the player is close to the end of the currently spawned tiles
        //if yes spawns new tile and deletes oldest one
        if (playerTransform.position.z - safeZone > spawnZ - amtTilesOnScreen * tileLength)
        {
            SpawnTile();
            SpawnCoins();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject gameOb;
        if (prefabIndex == -1)
            gameOb = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            gameOb = Instantiate(tilePrefabs[0]) as GameObject;

        gameOb.transform.SetParent(transform);
        gameOb.transform.position = Vector3.forward * spawnZ;

        NavMeshSurface navMeshSurface = gameOb.GetComponent<NavMeshSurface>();
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            SpawnEnemies(gameOb);
        }
        spawnZ += tileLength;
        activeTiles.Add(gameOb);
    }
    void SpawnEnemies(GameObject tile)
    {
        Vector3 enemySpawnPosition = tile.transform.position + new Vector3(0, 2f, 0); // Adjust Y position as needed
        Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity, tile.transform);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    public void SpawnCoins()
    {        
        GameObject lastTile = activeTiles[^2];

        int coinsToSpawn = Random.Range(0,3);
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, lastTile.transform);
            temp.transform.position = GetRandomPointInCollider(lastTile.GetComponent<Collider>());
        }
    }
    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            2,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );

        return point;
    }
}

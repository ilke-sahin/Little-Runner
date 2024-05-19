using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = -3.0f;
    private float tileLength = 7.0f;
    private float safeZone = 15.0f;
    private int amtTilesOnScreen = 5;
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
        spawnZ += tileLength;
        activeTiles.Add(gameOb);
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
}
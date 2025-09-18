using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    public TreasureItem itemToSpawn;

    public List<TreasureItem> itemList = new List<TreasureItem>();
    public List<TreasureItem> itemsToSpawn = new List<TreasureItem>();

    [Header("Spawn Settings")]
    public float spawnLoopInterval = 2f;
    public float spawnLoopTime;
    public Vector2 spawnArea1X;
    public Vector2 spawnArea1Y;
    public Vector2 spawnArea2X;
    public Vector2 spawnArea2Y;



    void SelectItemsToSpawn()
    {
        itemsToSpawn.Clear();

        foreach (TreasureItem item in itemList)
        {
            int spawnChance = Random.Range(0, 101);

            if (spawnChance > item.spawnChance) continue;

            itemsToSpawn.Add(item);
        }
    }

    void SpawnItems()
    {
        foreach (TreasureItem item in itemsToSpawn)
        {
            SpawnItem(item, GetSpawnLocation());
        }
    }

    void SpawnItem(TreasureItem item, Vector2 position)
    {
        TreasureItem newItem = Instantiate(item, position, Quaternion.identity, gameObject.transform);
    }

    Vector2 GetSpawnLocation()
    {
        float x1 = Random.Range(spawnArea1X.x, spawnArea1X.y);
        float y1 = Random.Range(spawnArea1Y.x, spawnArea1Y.y);
        float x2 = Random.Range(spawnArea2X.x, spawnArea2X.y);
        float y2 = Random.Range(spawnArea2Y.x, spawnArea2Y.y);

        Vector2 spawnPos1 = new Vector2(x1, y1);
        Vector2 spawnPos2 = new Vector2(x2, y2);

        int spawnSide = Random.Range(0, 2);

        Vector2 spawnPos = spawnSide < 1 ? spawnPos1 : spawnPos2;

        return spawnPos;
    }



    #region Unity lifecycle

    void Update()
    {
        spawnLoopTime += Time.deltaTime;

        if (spawnLoopTime >= spawnLoopInterval)
        {
            SelectItemsToSpawn();
            SpawnItems();

            spawnLoopTime = 0;
        }
    }
    
    #endregion
}

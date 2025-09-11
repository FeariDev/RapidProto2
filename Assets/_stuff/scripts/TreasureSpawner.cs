using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    public TreasureItem itemToSpawn;

    [Header("Spawn Settings")]
    public float spawnLoopInterval = 2f;
    public float spawnLoopTime;
    public Vector2 spawnAreaX;
    public Vector2 spawnAreaY;



    void SpawnItem(TreasureItem item, Vector2 position)
    {
        TreasureItem newItem = Instantiate(item, position, Quaternion.identity, gameObject.transform);
    }

    Vector2 GetSpawnLocation()
    {
        float x = Random.Range(spawnAreaX.x, spawnAreaX.y);
        float y = Random.Range(spawnAreaY.x, spawnAreaY.y);

        Vector2 spawnPos = new Vector2(x, y);

        return spawnPos;
    }



    #region Unity lifecycle

    void Update()
    {
        spawnLoopTime += Time.deltaTime;

        if (spawnLoopTime >= spawnLoopInterval)
        {
            SpawnItem(itemToSpawn, GetSpawnLocation());

            spawnLoopTime = 0;
        }
    }
    
    #endregion
}

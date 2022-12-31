using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    //suvalistame spawnimist:
    GameObject[] randomEnemyPrefab;
    public bool spawnRandomly = true;
    //spawnimiste vahelised intervallid:
    public float minSpawnInterval = 0.5f, maxSpawnInterval = 2f;
    
    public Tilemap floorTiles;
    public Tilemap wallTiles;
    public Tilemap ceilingTiles;

    public float minDistance = 3f;
    public float maxDistance = 5f;
    [HideInInspector] public int numberOfSpawned = 0;
    
    private void Awake()
    {
        StartCoroutine(SpawnLoop());
    }
    private void Start()
    {
        randomEnemyPrefab = GlobalReferences.listOfEnemyPrefabs;
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 50; i++)
        {
            /*Camera camera = GlobalReferences.mainCamera;
            float height = camera.orthographicSize;
            float width = height * camera.aspect;
            float distance = (height + width) * 0.75f;*/

            for (int j = 0; j < 100; j++)
            {
                float angle = UnityEngine.Random.Range(0f, 2f * (float)Math.PI);
                float distance = UnityEngine.Random.Range(minDistance, maxDistance);

                Vector3 vec = GlobalReferences.thePlayer.transform.position;
                vec.x += (float)Math.Sin(angle) * distance;
                vec.y += (float)Math.Cos(angle) * distance;

                if (CanSpawnAt(vec))
                {
                    if(spawnRandomly){
                        numberOfSpawned += 1;
                        Instantiate(randomEnemyPrefab[UnityEngine.Random.Range(0,randomEnemyPrefab.Length)], vec, new Quaternion(), transform);
                        break;
                    }
                    else{
                        //skoori järgi võtta see millist enemy't spawnida
                    }
                }
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    bool CanSpawnAt(Vector2 position)
    {
        if (!floorTiles.HasTile(floorTiles.WorldToCell(position)))
            return false;
        if (wallTiles.HasTile(wallTiles.WorldToCell(position)))
            return false;
        if (ceilingTiles.HasTile(ceilingTiles.WorldToCell(position)))
            return false;

        return true;
    }

}
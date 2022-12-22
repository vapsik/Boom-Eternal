using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{

    public Enemy enemyPrefab;
    public Tilemap floorTiles;
    public Tilemap wallTiles;
    public Tilemap ceilingTiles;

    public float minDistance = 3f;
    public float maxDistance = 5f;

    private void Awake()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(0f);

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
                    Instantiate(enemyPrefab, vec, new Quaternion(), transform);
                    break;
                }
            }

            yield return new WaitForSeconds(1f);
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
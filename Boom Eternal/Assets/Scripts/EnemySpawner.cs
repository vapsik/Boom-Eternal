using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Enemy enemyPrefab;

    private void Awake()
    {
        StartCoroutine(SpawnLoop());
    }


    IEnumerator SpawnLoop()
    {
        for (int i = 0; i < 20; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab);

            Camera camera = GlobalReferences.mainCamera;
            float height = camera.orthographicSize;
            float width = height * camera.aspect;
            float distance = (height + width) * 0.75f;

            System.Random random = new System.Random();
            float angle = Mathf.Deg2Rad * random.Next(360);

            Vector3 vec = enemy.transform.position;
            vec.x = (float)Math.Sin(angle) * distance;
            vec.y = (float)Math.Cos(angle) * distance;
            enemy.transform.position = vec;

            yield return new WaitForSeconds(2f);
        }
    }


}

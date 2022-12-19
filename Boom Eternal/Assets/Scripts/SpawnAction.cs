using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction
{

    public static SpawnAction Spawn(int enemyType, int amount, Vector2 position)
    {
        return Spawn(enemyType, amount, position, 0);
    }

    public static SpawnAction Spawn(int enemyType, int amount, Vector2 position, float posRandomRadius) {
        SpawnC spawnC = new SpawnC();
        spawnC.enemyType = enemyType;
        spawnC.amount = amount;
        spawnC.position = position;
        spawnC.posRandomRadius = posRandomRadius;
        return spawnC;
    }

    public static SpawnAction Wait(float time)
    {
        WaitC waitC = new WaitC();
        waitC.waitTime = time;
        return waitC;
    }

    public static SpawnAction WaitForWeight(int weight)
    {
        WaitForWeightC waitFOrWeightC = new WaitForWeightC();
        waitFOrWeightC.weight = weight;
        return waitFOrWeightC;
    }

    public class SpawnC : SpawnAction
    {
        public Vector2 position;
        public float posRandomRadius;
        public int enemyType;
        public int amount;
    }

    public class WaitC : SpawnAction
    {
        public float waitTime;
    }

    public class WaitForWeightC : SpawnAction
    {
        public int weight;
    }

}